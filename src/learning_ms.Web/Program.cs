using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using learning_ms.Web.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;

// ─── Load .env before anything reads configuration ────────────────────────────
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// ─── Serilog ──────────────────────────────────────────────────────────────────
builder.Host.UseSerilog((ctx, config) =>
    config
        .ReadFrom.Configuration(ctx.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"));

// ─── Bind .env values into configuration ─────────────────────────────────────
builder.Configuration.AddEnvironmentVariables();

// ─── Controllers + Swagger ───────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "School Management System API Develop By Asoh Yannick, .NET Developer.",
    Version = builder.Configuration["ApiSettings:Version"] ?? "v1",
    Description = "RESTful API for managing school operations — students, courses, enrollments, staff and more.",
    Contact = new OpenApiContact
    {
      Name = "Backend Team",
      Email = "dev@school.com"
    }
  });

  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Enter your JWT token. Example: eyJhbGci..."
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ─── PostgreSQL + EF Core ─────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsql => npgsql.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null))
    .UseSnakeCaseNamingConvention());

// ─── Application services (register here as you build) ───────────────────────
// builder.Services.AddScoped<IRepository<Student>, EfRepository<Student>>();
// builder.Services.AddMediator();

var app = builder.Build();

// ─── Verify PostgreSQL connection on startup ──────────────────────────────────
using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
  try
  {
    await db.Database.CanConnectAsync();
    logger.LogInformation("✅ PostgreSQL connection successful");
  }
  catch (Exception ex)
  {
    logger.LogError(ex, "❌ PostgreSQL connection failed — check your .env credentials");
    throw;
  }
}

// ─── Middleware pipeline ──────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
  app.UseSwagger(c =>
  {
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
  });

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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ─── Startup banner — logs after everything is wired ─────────────────────────
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
  Log.Information("  ⚙️   Environment : {Env}", app.Environment.EnvironmentName);
  Log.Information("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
});

app.Run();

public partial class Program;
