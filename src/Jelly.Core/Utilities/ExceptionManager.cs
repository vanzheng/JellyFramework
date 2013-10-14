using System;

namespace Jelly.Utilities
{
    public class ExceptionManager
    {
        public static void ThrowIfNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void ThrowIfNull<T>(object obj) where T : Exception, new() 
        {
            if (obj == null) 
            {
                throw new T();
            }
        }

        public static void ThrowIfEmpty(string str, string message) 
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void ThrowIfEmpty(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException();
            }
        }
    }
}
