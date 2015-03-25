using System;

namespace Taut
{
    internal static class ObjectExtensions
    {
        public static void ThrowIfNull(this object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
