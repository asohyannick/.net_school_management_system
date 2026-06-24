using learning_ms.Web.Application.Common.Settings.FileUploadSettings;
using learning_ms.Web.Application.Common.Settings.MinioSettings;
using learning_ms.Web.Application.Interface.IFileStorageService;
using Minio;
namespace learning_ms.Web.Infrastructure.FileStorage.MinioServiceExtensions;

public static class MinioServiceExtensions
{
  public static IServiceCollection AddMinioStorage(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    var minioSettings = configuration
        .GetSection("MinioSettings")
        .Get<MinioSettings>()
        ?? throw new InvalidOperationException("MinioSettings section is missing from configuration.");

    var uploadSettings = configuration
        .GetSection("FileUploadSettings")
        .Get<FileUploadSettings>()
        ?? new FileUploadSettings(); 

    services.AddSingleton(minioSettings);
    services.AddSingleton(uploadSettings);

    services.AddMinio(client => client
        .WithEndpoint(minioSettings.Endpoint)
        .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey)
        .WithSSL(minioSettings.UseSSL)
        .Build());

    services.AddScoped<IFileStorageService, MinioFileStorageService>();

    return services;
  }
}