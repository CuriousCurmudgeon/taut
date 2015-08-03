using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut
{
    public static class DateExtensions
    {
        public static double ToUtcUnixTimestamp(this DateTime dateTime)
        {
            // Make sure we do everything in UTC.
            return (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
