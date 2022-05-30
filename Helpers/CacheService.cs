using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Helpers
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public string Get(string key)
        {
            try
            {
                return _distributedCache.GetString(key);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        public T Get<T>(string key)
        {
            try
            {
                string get = Get(key);

                return JsonConvert.DeserializeObject<T>(get);
            }
            catch (Exception)
            {
                return default;
            }

        }

        public bool Remove(string key)
        {
            try
            {
                _distributedCache.Remove(key);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Set(string key, string value, TimeSpan time)
        {
            try
            {
                _distributedCache.SetString(key, value, new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = time
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Set<T>(string key, T value, TimeSpan time)
        {
            try
            {
                string set = JsonConvert.SerializeObject(value);

                return Set(key, set, time);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public interface ICacheService
    {
        public string Get(string key);
        public bool Remove(string key);
        public T Get<T>(string key);
        public bool Set(string key, string value, TimeSpan time);
        public bool Set<T>(string key, T value, TimeSpan time);
    }
}