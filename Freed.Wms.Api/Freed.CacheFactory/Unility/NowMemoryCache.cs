using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freed.CacheFactory.Unility
{
    public class NowMemoryCache : IMemoryCache
    {
        private IMemoryCache _memoryCache;

        public NowMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _memoryCache.Dispose();
        }

        public void Remove(object key)
        {
            _memoryCache.Remove(key);
        }

        public bool TryGetValue(object key, out object value)
        {
            return _memoryCache.TryGetValue(key,out value);
        }

        public void Set(object key,object value)
        {
            _memoryCache.Set(key, value, TimeSpan.FromSeconds(60));
        }
    }
}
