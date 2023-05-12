using System;
using Neredekal.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CacheHelper
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key) => (T)_memoryCache.Get(key);

        public T Set<T>(string key, T value, int expiration = 60) => 
            _memoryCache.Set<T>(key, value, new DateTimeOffset(DateTime.Now.AddMinutes(expiration)));

        public void Remove(string key) => _memoryCache.Remove(key);
    }
}