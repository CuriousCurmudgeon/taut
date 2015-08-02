using System;

namespace Taut
{
    internal static class Preconditions
    {
        public static void ThrowIfNull(this object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(this string str, string name)
        {
            if (str == null)
            {
                throw new ArgumentNullException(name);
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(name);
            }
        }
    }
}
