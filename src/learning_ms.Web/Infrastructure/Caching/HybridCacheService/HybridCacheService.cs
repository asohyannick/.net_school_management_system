using Microsoft.Extensions.Caching.Hybrid;
using learning_ms.Web.Application.Interface.ICacheService;
using learning_ms.Web.Infrastructure.Caching.CacheSettings;
public sealed class HybridCacheService : ICacheService
{
    private readonly HybridCache _cache;
    private readonly CacheSettings _settings;

    public HybridCacheService(HybridCache cache, CacheSettings settings)
    {
        _cache = cache;
        _settings = settings;
    }

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        IEnumerable<string>? tags = null,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        var options = new HybridCacheEntryOptions
        {
            Expiration = expiration ?? TimeSpan.FromMinutes(_settings.DefaultExpirationMinutes),
            LocalCacheExpiration = TimeSpan.FromMinutes(_settings.LocalCacheExpirationMinutes)
        };

        return await _cache.GetOrCreateAsync(
            key,
            async ct => await factory(ct),
            options,
            tags: tags?.ToArray(),
            cancellationToken: cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => await _cache.RemoveAsync(key, cancellationToken);

    public async Task RemoveByTagAsync(string tag, CancellationToken cancellationToken = default)
        => await _cache.RemoveByTagAsync(tag, cancellationToken);
}
