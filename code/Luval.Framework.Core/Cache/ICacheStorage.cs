using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public interface ICacheStorage<TKey, TValue>
    {

        Task<bool> ExistsAsync(TKey key);

        Task AddAsync(TKey key, TValue value, IExpirationPolicy expirationPolicy);
        Task RemoveAsync(TKey key);
        Task<ICacheStorageItem<TKey, TValue>> GetAsync(TKey key);
    }
}
