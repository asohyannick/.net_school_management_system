namespace learning_ms.Web.Infrastructure.Caching.CacheSettings;
public sealed class CacheSettings
{
  public const string SectionName = "CacheSettings";

  public string RedisConnectionString { get; set; } = string.Empty;

  public int DefaultExpirationMinutes { get; set; } = 5;
  
  public int LocalCacheExpirationMinutes { get; set; } = 1;

  public bool UseRedis { get; set; } = true;
}
