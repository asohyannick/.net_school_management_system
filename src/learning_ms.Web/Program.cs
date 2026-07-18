using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetEnv;
using FluentValidation;
using learning_ms.Web.Application.Exceptions.InternalServerError;
using learning_ms.Web.Application.Interface.IUserRepository;
using learning_ms.Web.Application.Mappings.AccommodationMapper;
using learning_ms.Web.Application.Mappings.AdmissionMapper;
using learning_ms.Web.Application.Mappings.AiChatBotMapper;
using learning_ms.Web.Application.Mappings.AnnouncementMapper;
using learning_ms.Web.Application.Mappings.AssignmentMapper;
using learning_ms.Web.Application.Mappings.AttendanceMapper;
using learning_ms.Web.Application.Mappings.BookLoanMapper;
using learning_ms.Web.Application.Mappings.BookMapper;
using learning_ms.Web.Application.Mappings.CourseMapper;
using learning_ms.Web.Application.Mappings.DiscussionForumMapper;
using learning_ms.Web.Application.Mappings.ExamMapper;
using learning_ms.Web.Application.Mappings.GradeMapper;
using learning_ms.Web.Application.Mappings.HumanResourceMapper;
using learning_ms.Web.Application.Mappings.LibraryMapper;
using learning_ms.Web.Application.Mappings.QuizAttemptMapper;
using learning_ms.Web.Application.Mappings.QuizMapper;
using learning_ms.Web.Application.Mappings.QuizOptionMapper;
using learning_ms.Web.Application.Mappings.StudentAccommodationMapper;
using learning_ms.Web.Application.Mappings.StudentProfileMapper;
using learning_ms.Web.Application.Mappings.TimeTableMapper;
using learning_ms.Web.Application.Mappings.TutorProfileMapper;
using learning_ms.Web.Application.Mappings.UserMapper;
using learning_ms.Web.Application.Validators.Admissions;
using learning_ms.Web.Domain.Entities.User;
using learning_ms.Web.Infrastructure.ApiResponse;
using learning_ms.Web.Infrastructure.Auth.FirebaseServiceCollectionExtensions;
using learning_ms.Web.Infrastructure.Auth.JwtServiceCollectionExtensions;
using learning_ms.Web.Infrastructure.BackgroundJobs;
using learning_ms.Web.Infrastructure.ConfigurationExtensions.ConfigurationExtensions;
using learning_ms.Web.Infrastructure.ConfigurationExtensions.RoleSeedSettingsExtensions;
using learning_ms.Web.Infrastructure.Email;
using learning_ms.Web.Infrastructure.FileStorage.MinioServiceExtensions;
using learning_ms.Web.Infrastructure.Payments.PayPal.PayPalServiceCollectionExtensions;
using learning_ms.Web.Infrastructure.Persistence;
using learning_ms.Web.Infrastructure.Persistence.Conventions;
using learning_ms.Web.Infrastructure.Persistence.Repositories.UserRepository;
using learning_ms.Web.Infrastructure.Persistence.Seeding;
using learning_ms.Web.Infrastructure.RateLimiting.RateLimitingServiceCollectionExtensions;
using learning_ms.Web.Presentation.ConfigurationExtensions.CorsServiceExtensions;
using learning_ms.Web.Presentation.Filters.TagDescriptionsDocumentFilter;
using learning_ms.Web.Presentation.Middleware.MiddlewareExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

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

  // ─── API version — single source of truth, reused everywhere below ────────
  var apiVersion = builder.Configuration["ApiSettings:Version"] ?? "v1";

  // ─── Controllers + JSON + global "api/{version}" route prefix ─────────────
  builder.Services
    .AddControllers(options =>
    {
      options.Conventions.Add(new RoutePrefixConvention($"api/{apiVersion}"));
    })
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
      options.JsonSerializerOptions.WriteIndented = false;
      options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
      );
    })
    .ConfigureApiBehaviorOptions(options =>
    {
      options.InvalidModelStateResponseFactory = context =>
      {
        var errors = context.ModelState
          .Where(kvp => kvp.Value?.Errors.Count > 0)
          .SelectMany(kvp => kvp.Value!.Errors.Select(e =>
            string.IsNullOrEmpty(e.ErrorMessage) ? "Invalid value." : e.ErrorMessage))
          .ToList();

        var response = ApiResponse<object>.FailureResponse(
          "One or more validation errors occurred.", errors, 400);

        return new BadRequestObjectResult(response);
      };
    });

  builder.Services.AddEndpointsApiExplorer();

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

  builder.Services.Configure<RoleSeedSettingsExtensions>(
    builder.Configuration.GetSection(RoleSeedSettingsExtensions.SectionName));

  // ─── OpenAPI / Swagger ────────────────────────────────────────────────────
  builder.Services.AddSwaggerGen(c =>
  {
    c.TagActionsBy(api =>
    {
      var tags = api.ActionDescriptor.EndpointMetadata
        .OfType<TagsAttribute>()
        .FirstOrDefault();

      return tags?.Tags.ToList()
             ?? new List<string>
             {
               api.GroupName ?? api.ActionDescriptor.RouteValues["controller"]!
             };
    });
    c.DocumentFilter<TagDescriptionsDocumentFilter>();
    c.SwaggerDoc(
      apiVersion,
      new OpenApiInfo
      {
        Title = "School Management System API — Built with ❤️ by Asoh Yannick .NET Developer .",
        Version = apiVersion,
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

    // ─── XML comments for Swagger descriptions ──────────────────────────────
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
      c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
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
  builder.Services.AddJwtAuthentication(builder.Configuration);
  builder.Services.AddFirebaseAuthentication(builder.Configuration);

  // ─── Mediator (source-generator based, not MediatR) ──────────────────────
  builder.Services.AddMediator(options =>
  {
    options.ServiceLifetime = ServiceLifetime.Scoped;
  });

  // ─── Auth / Identity ───────────────────────────────────────────────────────
  builder.Services.AddScoped<IUserRepository, UserRepository>();
  builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

  // ─── Application services (Mapping + Validation) ─────────────────────────
  builder.Services.AddValidatorsFromAssemblyContaining<CreateAdmissionRequestDtoValidator>();
  builder.Services.AddFluentValidationAutoValidation();
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
  builder.Services.AddScoped<BookMapper>();
  builder.Services.AddScoped<QuizAttemptMapper>();
  builder.Services.AddScoped<QuizQuestionMapper>();
  builder.Services.AddScoped<QuizOptionMapper>();
  builder.Services.AddScoped<QuizMapper>();
  builder.Services.AddScoped<StudentAccommodationMapper>();
  builder.Services.AddScoped<StudentProfileMapper>();
  builder.Services.AddScoped<TimeTableMapper>();
  builder.Services.AddScoped<TutorProfileMapper>();
  builder.Services.AddScoped<UserMapper>();

  // ─── HttpClient pooling ───────────────────────────────────────────────────
  builder.Services.AddHttpClient();

  // ─── Health checks ────────────────────────────────────────────────────────
  var redisConnectionString = builder.Configuration["CacheSettings:RedisConnectionString"];

  var healthChecksBuilder = builder.Services
    .AddHealthChecks()
    .AddNpgSql(
      builder.Configuration.GetConnectionString("DefaultConnection")!,
      name: "postgresql",
      tags: ["db", "ready"]
    );

  if (!string.IsNullOrWhiteSpace(redisConnectionString))
  {
    healthChecksBuilder.AddRedis(
      redisConnectionString,
      name: "redis",
      tags: ["cache", "ready"]
    );
  }
  else
  {
    Log.Warning("CacheSettings:RedisConnectionString is not set — skipping Redis health check.");
  }
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
      throw new InternalServerErrorException("❌ PostgreSQL connection failed — check your .env credentials", ex);
    } 
    finally 
    {
      await db.Database.CloseConnectionAsync();
    }
  }

  // ─── Role account seeding ─────────────────────────────────────────────────
  using (var seedScope = app.Services.CreateScope())
  {
    var db = seedScope.ServiceProvider.GetRequiredService<AppDbContext>();
    var passwordHasher = seedScope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
    var settingsOptions = seedScope.ServiceProvider.GetRequiredService<IOptions<RoleSeedSettingsExtensions>>();
    var logger = seedScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    await RoleAccountSeeder.SeedAsync(
      db, passwordHasher, settingsOptions, logger);
  }

  // ─── Middleware pipeline — ORDER IS CRITICAL ──────────────────────────────

  // 1. Dev tooling (never reaches production)
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint($"/swagger/{apiVersion}/swagger.json", $"School Management System API {apiVersion}");
      c.RoutePrefix = $"api/{apiVersion}/docs";
      c.DisplayRequestDuration();
      c.EnableDeepLinking();
      c.DocumentTitle = "SMS API Docs";
    });
  }

  app.UseGlobalExceptionHandler();

  app.UseResponseCompression();

  if (!app.Environment.IsDevelopment())
  {
    app.UseHttpsRedirection();
  }

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
    var urls = string.Join(" | ", app.Urls);

    Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
    Log.Information("  🏫  School Management System API");
    Log.Information("  📌  Version     : {Version}", apiVersion);
    Log.Information("  🌐  Listening   : {Urls}", urls);
    Log.Information("  📄  Swagger UI  : http://localhost:8000/api/{Version}/docs", apiVersion);
    Log.Information("  🚀  Scalar UI   : http://localhost:8000/scalar/{Version}", apiVersion);
    Log.Information("  ❤️   Health      : http://localhost:8000/health");
    Log.Information("  ⚙️   Environment : {Env}", app.Environment.EnvironmentName);
    Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
  });

  await app.RunAsync();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
  Log.Fatal(ex, "❌ Application terminated unexpectedly during startup");
  throw new InternalServerErrorException("❌Application terminated unexpectedly during startup", ex);
}
finally
{
  await Log.CloseAndFlushAsync();
}
