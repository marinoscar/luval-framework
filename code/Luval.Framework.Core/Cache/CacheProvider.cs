using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public class CacheProvider<TKey, TValue> : ICacheProvider<TKey, TValue>
    {
        public CacheProvider(string? name, IExpirationPolicyFactory expirationPolicyFactory, ICacheStorage<TKey, TValue> storage)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Storage = storage ?? throw new ArgumentNullException(nameof(storage));
            PolicyFactory = expirationPolicyFactory ?? throw new ArgumentNullException(nameof(expirationPolicyFactory));
        }

        public ICacheStorage<TKey, TValue> Storage { get; private set; }

        public string Name { get; private set; }

        public IExpirationPolicyFactory PolicyFactory { get; private set; }

        public async Task<TValue> GetAsync(TKey key, IExpirationPolicy expirationPolicy, Func<TKey, TValue> getValueFunction)
        {
            var item = default(TValue);
            var exists = await Storage.ExistsAsync(key);
            if (!exists)
            {
                item = await GetItemAndStoreAsync(key, expirationPolicy, getValueFunction);
                return item;
            }
            var cacheItem = await Storage.GetAsync(key);
            if (cacheItem.ExpirationPolicy.HasExpired())
            {
                item = await GetItemAndStoreAsync(key, expirationPolicy, getValueFunction);
            }
            return item;
        }

        private async Task<TValue?> GetItemAndStoreAsync(TKey key, IExpirationPolicy expirationPolicy, Func<TKey, TValue> getValueFunction)
        {
            TValue? item = getValueFunction(key) ?? throw new ArgumentException("Unable to retrieve item for cache", nameof(getValueFunction));
            await Storage.AddAsync(key, item, expirationPolicy);
            return item;
        }

        public Task<TValue> GetAsync(TKey key, Func<TKey, TValue> getValueFunction)
        {
            return GetAsync(key, this.PolicyFactory.Create(), getValueFunction);
        }
    }
}
