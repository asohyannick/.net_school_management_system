using learning_ms.Web.Application.Interface.IFirebaseAuthService;
namespace learning_ms.Web.Infrastructure.Auth.FirebaseServiceCollectionExtensions;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
public static class FirebaseServiceCollectionExtensions
{
  public static IServiceCollection AddFirebaseAuthentication(
    this IServiceCollection services, IConfiguration configuration)
  {
    var settings = configuration
                     .GetSection(FirebaseSettings.FirebaseSettings.SectionName)
                     .Get<FirebaseSettings.FirebaseSettings>()
                   ?? throw new InvalidOperationException(
                     $"Missing '{FirebaseSettings.FirebaseSettings.SectionName}' configuration section.");

    var privateKey = settings.PrivateKey.Replace("\\n", "\n");

    var initializer = new ServiceAccountCredential.Initializer(settings.ClientEmail)
    {
      ProjectId = settings.ProjectId
    }.FromPrivateKey(privateKey);

    var serviceAccountCredential = new ServiceAccountCredential(initializer);
    var credential = GoogleCredential.FromServiceAccountCredential(serviceAccountCredential);

    if (FirebaseApp.DefaultInstance is null)
    {
      FirebaseApp.Create(new AppOptions
      {
        Credential = credential,
        ProjectId = settings.ProjectId
      });
    }

    services.AddScoped<IFirebaseAuthService, FirebaseAuthService.FirebaseAuthService>();

    return services;
  }
}
