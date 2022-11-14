using System; 
using System.Collections.Generic;

namespace DataAPI.Cache
{
    public class CacheResponse<T>
    {
        public string Key { get; set; }
        public List<T> Value { get; set; }
        public Exception ex { get; set; }
        public string MessageAPI { get; set; }
        public string StatusAPI { get; set; }
    }

    public class CacherResponse<T>
    {
        public T Value { get; set; }
        public bool IsFound { get; set; }
    }

    public class CacheDataWrapper<T>
    {
        public T Value { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
