using System;

namespace Jelly.Helpers
{
    /// <summary>
    /// The simplified exception manager. 
    /// </summary>
    public static class ExceptionManager
    {
        /// <summary>
        /// If the input parameter is null, throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <param name="message">The exception message.</param>
        public static void ThrowIfNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        /// <summary>
        /// If the input parameter is null, throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="obj">The input object.</param>
        public static void ThrowIfNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// If the input parameter is null, throws exception.
        /// </summary>
        /// <typeparam name="T">The <see cref="Exception"/> inherited from.</typeparam>
        /// <param name="obj">The input object.</param>
        public static void ThrowIfNull<T>(object obj) where T : Exception, new() 
        {
            if (obj == null) 
            {
                throw new T();
            }
        }

        /// <summary>
        /// If the input parameter is null or empty, throws ArgumentNullException.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="message">The exception message.</param>
        public static void ThrowIfNullOrEmpty(string input, string message) 
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(message);
            }
        }

        /// <summary>
        /// If the input parameter is null or empty, throws ArgumentNullException.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="message">The exception message.</param>
        public static void ThrowIfNullOrEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException();
            }
        }
    }
}
