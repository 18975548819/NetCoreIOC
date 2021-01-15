using System;
using System.Collections.Generic;
using System.Text;

namespace Freed.CacheFactory.Unility
{
    public interface ICustomMemoryCache
    {
        void Remove(object key);
        bool TryGetValue(object key, out object value);
        void Set(object key, object value);
    }
}
