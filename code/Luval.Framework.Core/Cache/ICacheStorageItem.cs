using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public interface ICacheStorageItem<TKey, TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
        IExpirationPolicy ExpirationPolicy { get; }
    }
}
