using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class LocalCacheService
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;
        private const string CacheKeys = "8c26f75d-b0bb-49cd-a97e-24a0620dc003";

        private static string GetCacheKey(string key)
        {
            return string.Format("{0}-{1}", "", key);
        }

        public static void RemoveObjectFromCache(string key)
        {
            try
            {
                var cacheKey = GetCacheKey(key);
                if (Cache.Contains(cacheKey))
                {
                    Cache.Remove(cacheKey);
                    RemoveCacheKeyFromList(key);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void AddObjectToCache(string key, object value, int hours = 8766)
        {
            try
            {
                var cacheKey = GetCacheKey(key);
                RemoveObjectFromCache(key);
                Cache.Add(cacheKey, value, DateTime.Now.AddHours(hours));
                AddCacheKeyToList(key);
            }
            catch (Exception ex)
            {
            }
        }

        private static void AddCacheKeyToList(string key)
        {
            try
            {
                var cacheKeyList = GetObjectFromCache<List<string>>(CacheKeys) ?? new List<string>();
                if (cacheKeyList.Contains(key))
                {
                    cacheKeyList.Remove(key);
                }
                cacheKeyList.Add(key);
                if (Cache.Contains(GetCacheKey(CacheKeys)))
                {
                    Cache.Remove(GetCacheKey(CacheKeys));
                }
                Cache.Add(GetCacheKey(CacheKeys), cacheKeyList, DateTime.Now.AddYears(1));
            }
            catch (Exception ex)
            {
            }
        }

        public static T GetObjectFromCache<T>(string key) where T : class
        {
            try
            {
                var cacheKey = GetCacheKey(key);
                var items = Cache.GetCacheItem(cacheKey);
                if (items != null)
                {
                    return (T)items.Value;
                }
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        private static void RemoveCacheKeyFromList(string key)
        {
            try
            {
                var cacheKeyList = GetObjectFromCache<List<string>>(CacheKeys) ?? new List<string>();
                if (cacheKeyList.Contains(key))
                {
                    cacheKeyList.Remove(key);
                }
                if (Cache.Contains(GetCacheKey(CacheKeys)))
                {
                    Cache.Remove(GetCacheKey(CacheKeys));
                }
                Cache.Add(GetCacheKey(CacheKeys), cacheKeyList, DateTime.Now.AddYears(1));
            }
            catch (Exception ex)
            {
            }
        }
    }
}
