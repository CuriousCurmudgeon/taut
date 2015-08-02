using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut
{
    public static class BoolExtensions
    {
        public static int ToInt32(this bool b)
        {
            return Convert.ToInt32(b);
        }

        public static int? ToInt32(this bool? b)
        {
            return b.HasValue ? ToInt32(b.Value) : default(int?);
        }
    }
}
