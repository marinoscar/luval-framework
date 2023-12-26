using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public interface ICacheProvider<TKey, TValue>
    {
        /// <summary>
        /// Gets the name of the cache provider
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="IExpirationPolicyFactory"/>
        /// </summary>
        IExpirationPolicyFactory PolicyFactory { get; }

        /// <summary>
        /// Gets or sets the value in the cache provider
        /// </summary>
        /// <param name="key">The key element in the cache</param>
        /// <param name="expirationPolicy">The expiration policy</param>
        /// <param name="getValueFunction">The function to get the item if not present in the cache or if the policy requires it</param>
        /// <returns>The item from the cache</returns>
        Task<TValue> GetAsync(TKey key, IExpirationPolicy expirationPolicy, Func<TKey, TValue> getValueFunction);

        /// <summary>
        /// Gets or sets the value in the cache provider
        /// </summary>
        /// <param name="key">The key element in the cache</param>
        /// <param name="getValueFunction">The function to get the item if not present in the cache or if the policy requires it</param>
        /// <returns>The item from the cache</returns>
        Task<TValue> GetAsync(TKey key, Func<TKey, TValue> getValueFunction);
    }
}
