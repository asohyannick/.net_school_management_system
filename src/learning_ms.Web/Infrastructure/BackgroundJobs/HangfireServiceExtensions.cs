using Hangfire;
using Hangfire.PostgreSql;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IFileProcessingService;
using learning_ms.Web.Application.Interface.IFileUploadJob;
using learning_ms.Web.Infrastructure.FileStorage.TempFileCleanupJob;

namespace learning_ms.Web.Infrastructure.BackgroundJobs;

public static class HangfireServiceExtensions
{
  private static bool IsInvalidConnectionString(string? connectionString)
  {
    if (string.IsNullOrWhiteSpace(connectionString))
      return true;

    if (connectionString.Contains("${", StringComparison.Ordinal))
      return true;

    return connectionString.Contains("placeholder", StringComparison.OrdinalIgnoreCase);
  }

  public static IServiceCollection AddHangfireBackgroundJobs(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var rawConnectionString =
      configuration.GetConnectionString("DefaultConnection")
      ?? throw new NotFoundException(
        "DefaultConnection string is missing from configuration."
      );

    var connectionString = rawConnectionString
      .Replace("${DATABASE_HOST}", configuration["DATABASE_HOST"] ?? string.Empty)
      .Replace("${DATABASE_PORT}", configuration["DATABASE_PORT"] ?? string.Empty)
      .Replace("${DATABASE_NAME}", configuration["DATABASE_NAME"] ?? string.Empty)
      .Replace("${DATABASE_USER}", configuration["DATABASE_USER"] ?? string.Empty)
      .Replace("${DATABASE_PASSWORD}", configuration["DATABASE_PASSWORD"] ?? string.Empty);

    var connectionStringHasUnresolvedValue = IsInvalidConnectionString(connectionString);


    services.AddSingleton(new HangfireConnectionStatus(connectionStringHasUnresolvedValue));

    services.AddHangfire(config =>
      config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(
          options => options.UseNpgsqlConnection(connectionString),
          new PostgreSqlStorageOptions
          {
            QueuePollInterval = TimeSpan.FromSeconds(5),
            JobExpirationCheckInterval = TimeSpan.FromHours(1),
            InvisibilityTimeout = TimeSpan.FromMinutes(30),
            DistributedLockTimeout = TimeSpan.FromMinutes(1),
            SchemaName = "hangfire",
          }
        )
    );

    services.AddHangfireServer(options =>
    {
      options.ServerName = $"file-processor:{Environment.MachineName}";
      options.WorkerCount = 3;
      options.Queues = ["file-processing", "maintenance", "default"];
      options.ShutdownTimeout = TimeSpan.FromMinutes(2);
    });

    services.AddScoped<IFileUploadJob, FileUploadJob>();
    services.AddScoped<TempFileCleanupJob>();

    services.AddScoped<IFileProcessingService, FileProcessingService>();

    return services;
  }

  public static WebApplication UseHangfireBackgroundJobs(
    this WebApplication app,
    IConfiguration configuration
  )
  {
    app.MapHangfireDashboard(
      "/hangfire",
      new DashboardOptions { DashboardTitle = "Background Jobs — File Processing" }
    );

    var connectionStatus = app.Services.GetRequiredService<HangfireConnectionStatus>();

    if (connectionStatus.HasUnresolvedConnectionString)
    {
      var logger = app
        .Services.GetRequiredService<ILoggerFactory>()
        .CreateLogger("HangfireServiceExtensions");
      logger.LogWarning(
        "Skipping recurring job registration for 'temp-file-cleanup' because "
          + "ConnectionStrings:DefaultConnection is unresolved. Register it manually once "
          + "the database is reachable (e.g. via the Hangfire dashboard) or restart the app "
          + "after setting the required environment variables."
      );
    }
    else
    {
      RecurringJob.AddOrUpdate<TempFileCleanupJob>(
        recurringJobId: "temp-file-cleanup",
        queue: "maintenance",
        methodCall: job => job.ExecuteAsync(),
        cronExpression: "*/30 * * * *",
        options: new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc }
      );
    }

    return app;
  }
}

file sealed class HangfireConnectionStatus(bool hasUnresolvedConnectionString)
{
  public bool HasUnresolvedConnectionString { get; } = hasUnresolvedConnectionString;
}
