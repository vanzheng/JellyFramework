using System;

namespace Jelly.Utilities
{
    public class ExceptionManager
    {
        public static void ThrowIfNull(object obj, string parameterName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void ThrowIfNull<T>(object obj) where T : Exception, new() 
        {
            if (obj == null) 
            {
                throw new T();
            }
        }

        public static void ThrowIfEmpty(string str, string parameterName) 
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
