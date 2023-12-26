using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public interface ICacheHost
    {
        ICacheProvider<TKey, TValue> GetProvider<TKey, TValue>(string name);

    }
}
