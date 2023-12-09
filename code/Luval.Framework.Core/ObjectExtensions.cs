using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T o)
        {
            var clone = Activator.CreateInstance(typeof(T));
            foreach (var p in o.GetType().GetProperties())
            {
                if(p.CanWrite)
                    p.SetValue(clone, p.GetValue(o));
            }
            return (T)Convert.ChangeType(clone, typeof(T));
        }
    }
}
