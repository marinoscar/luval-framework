using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core.Cache
{
    public interface IExpirationPolicy
    {
        bool HasExpired();

        string Serialize();

        IExpirationPolicy DeSerialize(string content);
    }
}
