using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetEnv;
using FluentValidation;
using learning_ms.Web.Application.Mappings.AccommodationMapper;
using learning_ms.Web.Application.Mappings.AdmissionMapper;
using learning_ms.Web.Application.Mappings.AiChatBotMapper;
using learning_ms.Web.Application.Mappings.AnnouncementMapper;
using learning_ms.Web.Application.Mappings.AssignmentMapper;
using learning_ms.Web.Application.Mappings.AttendanceMapper;
using learning_ms.Web.Application.Mappings.BookLoanMapper;
using learning_ms.Web.Application.Mappings.CourseMapper;
using learning_ms.Web.Application.Mappings.DiscussionForumMapper;
using learning_ms.Web.Application.Mappings.ExamMapper;
using learning_ms.Web.Application.Mappings.GradeMapper;
using learning_ms.Web.Application.Mappings.HumanResourceMapper;
using learning_ms.Web.Application.Mappings.LibraryMapper;
using learning_ms.Web.Application.Validators.Admissions;
using learning_ms.Web.Infrastructure.BackgroundJobs;
using learning_ms.Web.Infrastructure.ConfigurationExtensions;
using learning_ms.Web.Infrastructure.Email;
using learning_ms.Web.Infrastructure.FileStorage.MinioServiceExtensions;
using learning_ms.Web.Infrastructure.Payments.PayPal.PayPalServiceCollectionExtensions;
using learning_ms.Web.Infrastructure.Persistence;
using learning_ms.Web.Infrastructure.RateLimiting.RateLimitingServiceCollectionExtensions;
using learning_ms.Web.Presentation.Extensions.CorsServiceExtensions;
using learning_ms.Web.Presentation.Middleware.MiddlewareExtensions;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Serilog;

// ─── Bootstrap logger — captures startup errors before Serilog is fully configured ──
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
  Env.TraversePath().Load();

  var builder = WebApplication.CreateBuilder(args);

  // ─── Serilog ──────────────────────────────────────────────────────────────
  builder.Host.UseSerilog(
    (ctx, services, config) =>
      config
        .ReadFrom.Configuration(ctx.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName()
        .WriteTo.Console(
          outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        )
  );

  builder.Configuration.AddEnvironmentVariables();
  builder.Configuration.ResolveEnvironmentVariables();

  // ─── Controllers + JSON ───────────────────────────────────────────────────
  builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
      options.JsonSerializerOptions.WriteIndented = false;
      options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
      );
    });

  builder.Services.ConfigureHttpJsonOptions(options =>
  {
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.WriteIndented = false;
    options.SerializerOptions.Converters.Add(
      new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
    );
  });

  // ─── OpenAPI / Swagger ────────────────────────────────────────────────────
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc(
      "v1",
      new OpenApiInfo
      {
        Title = "School Management System API — Built with ❤️ by Asoh Yannick .NET Developer .",
        Version = builder.Configuration["ApiSettings:Version"] ?? "v1",
        Description =
          "RESTful API for managing school operations — students, courses, enrollments, staff and more.",
        Contact = new OpenApiContact { Name = "Backend Team", Email = "dev@school.com" },
      }
    );

    c.AddSecurityDefinition(
      "Bearer",
      new OpenApiSecurityScheme
      {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token. Example: eyJhbGci...",
      }
    );

    c.AddSecurityRequirement(
      new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference { 
              Type = ReferenceType.SecurityScheme, Id = "Bearer" 
            },
          },
          Array.Empty<string>()
        },
      }
    );
  });

  // ─── Database ─────────────────────────────────────────────────────────────
  builder.Services.AddDbContext<AppDbContext>(options =>
    options
      .UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsql =>
          npgsql.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null
          )
      )
      .UseSnakeCaseNamingConvention()
      .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()) 
      .EnableDetailedErrors(builder.Environment.IsDevelopment())
  );

  builder.Services.AddCachingInfrastructure(builder.Configuration);
  builder.Services.AddHybridCache(options =>
  {
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
      Expiration = TimeSpan.FromMinutes(30),
      LocalCacheExpiration = TimeSpan.FromMinutes(5),
    };
    options.MaximumPayloadBytes = 1024 * 1024;
  });

  builder.Services.AddOutputCache(options =>
  {
    options.AddBasePolicy(policy =>
      policy.Expire(TimeSpan.FromSeconds(60)).With(r => r.HttpContext.Request.Method == "GET")
    );

    options.AddPolicy("Short", policy => policy.Expire(TimeSpan.FromSeconds(30)));
    options.AddPolicy("Long", policy => policy.Expire(TimeSpan.FromHours(1)).Tag("long-cache"));
    options.AddPolicy("NoCache", policy => policy.NoCache());
  });

  // ─── Response Compression ────────────────────────────────────────────────
  builder.Services.AddResponseCompression(options =>
  {
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat([
      "application/json",
      "application/problem+json",
    ]);
  });
  builder.Services.Configure<BrotliCompressionProviderOptions>(o =>
    o.Level = CompressionLevel.Fastest
  );
  builder.Services.Configure<GzipCompressionProviderOptions>(o =>
    o.Level = CompressionLevel.Fastest
  );

  // ─── Infrastructure services ──────────────────────────────────────────────
  builder.Services.AddEmailInfrastructure(builder.Configuration);
  builder.Services.AddStripeConfiguration(builder.Configuration);
  builder.Services.AddPayPalConfiguration(builder.Configuration);
  builder.Services.AddRateLimitingInfrastructure(builder.Configuration);
  builder.Services.AddCorsPolicies(builder.Configuration);
  builder.Services.AddMinioStorage(builder.Configuration);
  builder.Services.AddHangfireBackgroundJobs(builder.Configuration);

  // ─── Application services (Mapping + Validation) ─────────────────────────
  builder.Services.AddValidatorsFromAssemblyContaining<CreateAdmissionRequestDtoValidator>();
  builder.Services.AddScoped<AdmissionMapper>();
  builder.Services.AddScoped<AccommodationMapper>();
  builder.Services.AddScoped<AiChatBotMapper>();
  builder.Services.AddScoped<AnnouncementMapper>();
  builder.Services.AddScoped<AssignmentMapper>();
  builder.Services.AddScoped<AttendanceMapper>();
  builder.Services.AddScoped<BookLoanMapper>();
  builder.Services.AddScoped<CourseMapper>();
  builder.Services.AddScoped<DiscussionForumMapper>();
  builder.Services.AddScoped<ExamMapper>();
  builder.Services.AddScoped<GradeMapper>();
  builder.Services.AddScoped<HumanResourceMapper>();
  builder.Services.AddScoped<LibraryMapper>();

  // ─── HttpClient pooling ───────────────────────────────────────────────────
  builder.Services.AddHttpClient();

  // ─── Health checks ────────────────────────────────────────────────────────
  var connectionString = builder
    .Services.AddHealthChecks()
    .AddNpgSql(
      builder.Configuration.GetConnectionString("DefaultConnection")!,
      name: "postgresql",
      tags: ["db", "ready"]
    )
    .AddRedis(
      builder.Configuration["CacheSettings:RedisConnectionString"]!,
      name: "redis",
      tags: ["cache", "ready"]
    );
  // ─────────────────────────────────────────────────────────────────────────
  var app = builder.Build();
  // ─────────────────────────────────────────────────────────────────────────

  // ─── Database connectivity check ──────────────────────────────────────────
  using (var scope = app.Services.CreateScope())
  {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
      await db.Database.OpenConnectionAsync();

      await using var command = db.Database.GetDbConnection().CreateCommand();
      command.CommandText = "SELECT 1";

      await command.ExecuteScalarAsync();

      logger.LogInformation("✅ PostgreSQL connection successful");
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "❌ PostgreSQL connection failed — check your .env credentials");
      throw;
    } 
    finally 
    {
      await db.Database.CloseConnectionAsync();
    }
  }

  // ─── Middleware pipeline — ORDER IS CRITICAL ──────────────────────────────

  // 1. Dev tooling (never reaches production)
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management System API v1");
      c.RoutePrefix = "api/v1/docs";
      c.DisplayRequestDuration();
      c.EnableDeepLinking();
      c.DocumentTitle = "SMS API Docs";
    });
    app.MapScalarApiReference(options =>
    {
      options.Title = "School Management System API";
      options.Theme = ScalarTheme.DeepSpace;
    });
  }

  app.UseGlobalExceptionHandler();

  app.UseResponseCompression();

  app.UseHttpsRedirection();

  app.UseCorsPolicy(builder.Configuration);

  app.UseOutputCache();

  app.UseRateLimiter();

  app.UseAuthentication();
  app.UseAuthorization();

  app.UseHangfireBackgroundJobs(builder.Configuration);

  app.MapHealthChecks("/health");

  app.MapControllers();

  // ─── Startup banner ───────────────────────────────────────────────────────
  app.Lifetime.ApplicationStarted.Register(() =>
  {
    var apiVersion = builder.Configuration["ApiSettings:Version"] ?? "v1";
    var urls = string.Join(" | ", app.Urls);

    Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
    Log.Information("  🏫  School Management System API");
    Log.Information("  📌  Version     : {Version}", apiVersion);
    Log.Information("  🌐  Listening   : {Urls}", urls);
    Log.Information("  📄  Swagger UI  : http://localhost:8000/api/v1/docs");
    Log.Information("  🚀  Scalar UI   : http://localhost:8000/scalar/v1");
    Log.Information("  ❤️   Health      : http://localhost:8000/health");
    Log.Information("  ⚙️   Environment : {Env}", app.Environment.EnvironmentName);
    Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
  });

  await app.RunAsync();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
  Log.Fatal(ex, "❌ Application terminated unexpectedly during startup");
}
finally
{
  await Log.CloseAndFlushAsync();
}
