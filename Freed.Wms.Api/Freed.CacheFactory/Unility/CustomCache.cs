using System;
using System.Collections.Generic;

namespace Freed.CacheFactory.Unility
{
    /// <summary>
    /// 自定义缓存
    /// </summary>
    public class CustomCache
    {
        private static Dictionary<string, object> CustomCacheDictionary = new Dictionary<string, object>();

        public static T Get<T>(string key)
        {
            return (T)CustomCacheDictionary[key];
        }

        public static bool Exists(string key)
        {
            return CustomCacheDictionary.ContainsKey(key);
        }

        public static void Remove(string key)
        {
            CustomCacheDictionary.Remove(key);
        }

        public static void Add(string key, object value)
        {
            CustomCacheDictionary.Add(key,value);
        }
    }
}
