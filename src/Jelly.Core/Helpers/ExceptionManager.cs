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
        /// <param name="paramName">The name of parameter that caused the expection.</param>
        /// <param name="message">The exception message.</param>
        public static void ThrowIfNull(object obj, string paramName, string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        /// <summary>
        /// If the input parameter is null, throws <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <param name="paramName">The name of parameter that caused the expection.</param>
        public static void ThrowIfNull(object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
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
        /// <param name="paramName">The name of parameter that caused the expection.</param>
        /// <param name="message">The exception message.</param>
        public static void ThrowIfNullOrEmpty(string input, string paramName, string message)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        /// <summary>
        /// If the input parameter is null or empty, throws ArgumentNullException.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="paramName">The name of parameter that caused the expection.</param>
        public static void ThrowIfNullOrEmpty(string input, string paramName) 
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// If the input parameter is null or empty, throws ArgumentNullException.
        /// </summary>
        /// <param name="input">The input string.</param>
        public static void ThrowIfNullOrEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException();
            }
        }
    }
}
