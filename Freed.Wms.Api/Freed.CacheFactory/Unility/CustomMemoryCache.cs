using System;
using System.Collections.Generic;
using System.Text;

namespace Freed.CacheFactory.Unility
{
    /// <summary>
    /// 抽象
    /// </summary>
    public class CustomMemoryCache : ICustomMemoryCache
    {
        public void Remove(object key)
        {
            CustomCache.Remove(key?.ToString());
        }

        public void Set(object key, object value)
        {
            CustomCache.Add(key?.ToString(), value);
        }

        public bool TryGetValue(object key, out object value)
        {
            if (!CustomCache.Exists(key?.ToString()))
            {
                value = null;
                return false;
            }
            else
            {
                value = CustomCache.Get<object>(key?.ToString());
                return true;
            }
        }
    }
}
