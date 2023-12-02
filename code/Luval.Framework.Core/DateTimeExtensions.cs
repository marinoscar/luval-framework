using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Framework.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime TrimMs(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
        }

        public static DateTime TrimSec(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);
        }

        public static DateTime TrimMin(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, 0, 0);
        }
    }
}
