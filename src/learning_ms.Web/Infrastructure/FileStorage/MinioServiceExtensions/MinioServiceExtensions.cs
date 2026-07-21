using learning_ms.Web.Application.Common.Settings.FileUploadSettings;
using learning_ms.Web.Application.Common.Settings.MinioSettings;
using learning_ms.Web.Application.Exceptions.NotFoundException;
using learning_ms.Web.Application.Interface.IFileStorageService;
using Minio;
namespace learning_ms.Web.Infrastructure.FileStorage.MinioServiceExtensions;
public static class MinioServiceExtensions
{
  public static IServiceCollection AddMinioStorage(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    var logger = loggerFactory.CreateLogger("MinioServiceExtensions");

    static bool IsUnresolved(string? value) =>
      string.IsNullOrWhiteSpace(value) || value.Contains("${", StringComparison.Ordinal);

    // ---- MinioSettings ----
    var section = configuration.GetSection("MinioSettings");

    if (!section.Exists())
    {
      throw new NotFoundException("MinioSettings section is missing from configuration.");
    }

    var endpoint = section["Endpoint"];
    var minioUsername = section["Minio_username"];
    var minioPassword = section["Minio_password"];
    var bucketName = section["BucketName"];
    var region = section["Region"];
    var useSslRaw = section["UseSSL"];

    var useSsl = true; // safe default
    if (!IsUnresolved(useSslRaw) && bool.TryParse(useSslRaw!.Trim(), out var parsedSsl))
    {
      useSsl = parsedSsl;
    }

    var hasUnresolvedValue =
      IsUnresolved(endpoint)
      || IsUnresolved(minioUsername)
      || IsUnresolved(minioPassword)
      || IsUnresolved(bucketName);

    var minioSettings = new MinioSettings
    {
      Endpoint = IsUnresolved(endpoint) ? "localhost:9000" : endpoint!,
      AccessKey = IsUnresolved(minioUsername) ? "unresolved" : minioUsername!,
      SecretKey = IsUnresolved(minioPassword) ? "unresolved" : minioPassword!,
      BucketName = IsUnresolved(bucketName) ? "unresolved" : bucketName!,
      Region = string.IsNullOrWhiteSpace(region) || IsUnresolved(region) ? "us-east-1" : region,
      UseSSL = useSsl,
    };

    var uploadSection = configuration.GetSection("FileUploadSettings");

    if (!uploadSection.Exists())
    {
      throw new NotFoundException("FileUploadSettings section is missing from configuration.");
    }

    var uploadSettings = new FileUploadSettings();

    var maxFileSizeRaw = uploadSection["MaxFileSizeBytes"];

    if (!IsUnresolved(maxFileSizeRaw) && long.TryParse(maxFileSizeRaw, out var parsedMaxFileSize))
    {
      uploadSettings.MaxFileSizeBytes = parsedMaxFileSize;
    }
    else
    {
      logger.LogWarning(
        "FileUploadSettings:MaxFileSizeBytes still contains an unresolved \"${{VAR}}\" placeholder or is invalid. "
          + "Falling back to default of {DefaultBytes} bytes until FILE_MAX_SIZE_BYTES is set in your environment.",
        uploadSettings.MaxFileSizeBytes
      );
    }

    var allowedTypesSection = uploadSection.GetSection("AllowedTypes");
    if (allowedTypesSection.Exists())
    {
      try
      {
        var configuredAllowedTypes = allowedTypesSection.Get<Dictionary<string, string[]>>();
        if (configuredAllowedTypes is { Count: > 0 })
        {
          uploadSettings.AllowedTypes = configuredAllowedTypes;
        }
      }
      catch (Exception ex)
      {
        logger.LogWarning(
          ex,
          "FileUploadSettings:AllowedTypes could not be bound from configuration. Falling back to default allowed types."
        );
      }
    }

    services.AddSingleton(minioSettings);
    services.AddSingleton(uploadSettings);

    if (hasUnresolvedValue)
    {
      logger.LogWarning(
        "MinioSettings:Endpoint/AccessKey/SecretKey/BucketName contains an unresolved \"${{VAR}}\" placeholder. "
          + "File storage will fail until MINIO_ENDPOINT, MINIO_USERNAME, MINIO_PASSWORD and MINIO_BUCKET_NAME are set in your environment."
      );
    }

    services.AddMinio(client =>
      client
        .WithEndpoint(minioSettings.Endpoint)
        .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey)
        .WithSSL(minioSettings.UseSSL)
        .Build()
    );

    services.AddScoped<IFileStorageService, MinioFileStorageService>();

    return services;
  }
}
