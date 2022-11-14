using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace DataAPI.Cache
{
    public class RuntimeMemoryCacher : IMemoryCacher
    {
        public object GetValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(key);
        }

        public bool Add(string key, object value, DateTimeOffset absExpiration)
        {
            Delete(key);
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, value, absExpiration);
        }

        public void Delete(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }

        public async Task Add<T>(string itemKey, T itemValue, DateTime cacheExpiration)
        {
            try
            {
                 Add(itemKey, itemValue, new DateTimeOffset(cacheExpiration));
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<CacherResponse<T>> GetValue<T>(string itemKey)
        {
            CacherResponse<T> cacherResponse = new CacherResponse<T>();
            try
            {
                T value=(T)GetValue(itemKey);

                if (value != null)
                {
                    cacherResponse.IsFound = true;
                    cacherResponse.Value = value;
                }

                return cacherResponse;
            }
            catch (Exception ex)
            { 
            }

            return cacherResponse;
        }

    }
}
