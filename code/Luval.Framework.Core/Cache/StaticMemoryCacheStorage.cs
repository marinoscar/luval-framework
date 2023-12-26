using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public class StaticMemoryCacheStorage<TKey, TValue> : ICacheStorage<TKey, TValue>
    {
        public static Dictionary<TKey, ICacheStorageItem<TKey, TValue>> _items = new Dictionary<TKey, ICacheStorageItem<TKey, TValue>>();


        public Task AddAsync(TKey key, TValue value, IExpirationPolicy expirationPolicy)
        {
            if (_items.ContainsKey(key))
            {
                _items[key] = new StaticMemoryCacheItem<TKey, TValue>() { Value = value, ExpirationPolicy = expirationPolicy, Key = key };
            }
            else
            {
                _items.Add(key, new StaticMemoryCacheItem<TKey, TValue>() { Value = value, ExpirationPolicy = expirationPolicy, Key = key });
            }
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(TKey key)
        {
            return Task.Run(() => { return _items.ContainsKey(key); });
        }

        public Task<ICacheStorageItem<TKey, TValue>> GetAsync(TKey key)
        {
            return Task.Run(() => { return _items[key]; });
        }

        public Task RemoveAsync(TKey key)
        {

            if (_items.ContainsKey(key))
            {
                _items.Remove(key);
            }
            return Task.CompletedTask;
        }
    }

    public class StaticMemoryCacheItem<TKey, TValue> : ICacheStorageItem<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public IExpirationPolicy ExpirationPolicy { get; set; }
    }
}
