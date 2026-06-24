using learning_ms.Web.Application.Interface.ICacheService;
using learning_ms.Web.Infrastructure.Caching.CacheSettings;
public static class CachingServiceCollectionExtensions
{
  public static IServiceCollection AddCachingInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    var settings = BindCacheSettings(configuration);
    services.AddSingleton(settings);

    if (settings.UseRedis && !string.IsNullOrWhiteSpace(settings.RedisConnectionString))
    {
      services.AddStackExchangeRedisCache(options =>
      {
        options.Configuration = settings.RedisConnectionString;
        options.InstanceName = "learning_ms:";
      });
    }

    services.AddHybridCache(options =>
    {
      options.DefaultEntryOptions = new()
      {
        Expiration = TimeSpan.FromMinutes(settings.DefaultExpirationMinutes),
        LocalCacheExpiration = TimeSpan.FromMinutes(settings.LocalCacheExpirationMinutes)
      };
    });

    services.AddSingleton<ICacheService, HybridCacheService>();

    return services;
  }


  private static CacheSettings BindCacheSettings(IConfiguration configuration)
  {
    var section = configuration.GetSection(CacheSettings.SectionName);

    return new CacheSettings
    {
      RedisConnectionString = ReadString(section, nameof(CacheSettings.RedisConnectionString), string.Empty),
      UseRedis = ReadBool(section, nameof(CacheSettings.UseRedis), defaultValue: true),
      DefaultExpirationMinutes = ReadInt(section, nameof(CacheSettings.DefaultExpirationMinutes), defaultValue: 5),
      LocalCacheExpirationMinutes = ReadInt(section, nameof(CacheSettings.LocalCacheExpirationMinutes), defaultValue: 1)
    };
  }

  private static string ReadString(IConfiguration section, string key, string defaultValue)
  {
    var raw = section[key];
    return IsUnresolvedPlaceholder(raw) ? defaultValue : raw!;
  }

  private static bool ReadBool(IConfiguration section, string key, bool defaultValue)
  {
    var raw = section[key];
    if (IsUnresolvedPlaceholder(raw)) return defaultValue;
    return bool.TryParse(raw, out var parsed) ? parsed : defaultValue;
  }

  private static int ReadInt(IConfiguration section, string key, int defaultValue)
  {
    var raw = section[key];
    if (IsUnresolvedPlaceholder(raw)) return defaultValue;
    return int.TryParse(raw, out var parsed) ? parsed : defaultValue;
  }

  private static bool IsUnresolvedPlaceholder(string? raw) =>
      string.IsNullOrWhiteSpace(raw) || (raw.StartsWith("${") && raw.EndsWith('}'));
}