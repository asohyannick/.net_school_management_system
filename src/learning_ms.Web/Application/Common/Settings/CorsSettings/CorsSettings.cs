namespace learning_ms.Web.Application.Common.Settings.CorsSettings;

public class CorsSettings
{
  public string PolicyName { get; set; } = "DefaultCorsPolicy";

  public string[] AllowedOrigins { get; set; } = [];
  public string[] AllowedMethods { get; set; } =
      ["GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS"];

  public string[] AllowedHeaders { get; set; } =
      ["Content-Type", "Authorization", "X-Requested-With", "Accept", "Origin"];

  public string[] ExposedHeaders { get; set; } =
      ["X-Pagination", "X-Total-Count", "Location"];

  public bool AllowCredentials { get; set; } = true;
  public int PreflightMaxAgeSeconds { get; set; } = 600;
}