using BusinessServiceTemplate.Shared.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BusinessServiceTemplate.Core.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        public CacheManager(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            var absoluteExpirationInSeconds = configuration.GetValue<int>("CacheSettings:ExpiryInSecs", 300);
            var slidingExpirationInSeconds = configuration.GetValue<int>("CacheSettings:SlidingExpiryInSecs", 60);
            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absoluteExpirationInSeconds),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationInSeconds)
            };
        }
        public object this[string category, string key]
        {
            get => _memoryCache.Get(CreateCacheKey(category, key));
            set => _memoryCache.Set(CreateCacheKey(category, key), value, _cacheEntryOptions);
        }

        private string CreateCacheKey(string category, string key)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException(ConstantStrings.CACHE_REQUEST_ERROR_MESSAGE);
            }

            return $"{category}_{key}";
        }
    }
}
