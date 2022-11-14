
using System; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAPI.Cache
{
    public interface IMemoryCacher
    {
        Task<CacherResponse<T>> GetValue<T>(string itemKey);
        Task Add<T>(string itemKey, T itemValue, DateTime cacheExpiration);
    }
}

