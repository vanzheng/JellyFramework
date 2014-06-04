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
        public static void ThrowArgumentNullExceptionIfNull(object obj, string paramName, string message)
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
        public static void ThrowArgumentNullExceptionIfNull(object obj, string paramName)
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
        public static void ThrowArgumentNullExceptionIfNull(object obj)
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
        /// If the input parameter is null, throws exception.
        /// </summary>
        /// <typeparam name="T">The <see cref="Exception"/> inherited from.</typeparam>
        /// <param name="obj">The condition for meets.</param>
        public static void ThrowIfNull<T>(bool condition) where T : Exception, new()
        {
            if (condition)
            {
                throw new T();
            }
        }

        /// <summary>
        /// If meets the condition, throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="condition">The condition for meets.</param>
        /// <param name="paramName">The name of parameter that caused the expection.</param>
        /// <param name="message">The exception message.</param>
        public static void ThrowArgumentExceptionIfMeet(bool condition, string paramName, string message)
        {
            if (condition)
            {
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// If meets the condition, throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="condition">The condition for meets.</param>
        /// <param name="message">The exception message.</param>
        public static void ThrowArgumentExceptionIfMeet(bool condition, string message)
        {
            if (condition)
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// If meets the condition, throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="condition">The condition for meets.</param>
        public static void ThrowArgumentExceptionIfMeet(bool condition)
        {
            if (condition)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// If the input parameter is null or empty, throws ArgumentNullException.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="paramName">The name of parameter that caused the expection.</param>
        /// <param name="message">The exception message.</param>
        public static void ThrowArgumentNullExceptionIfNullOrEmpty(string input, string paramName, string message)
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
        public static void ThrowArgumentNullExceptionIfNullOrEmpty(string input, string paramName) 
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
        public static void ThrowArgumentNullExceptionIfNullOrEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException();
            }
        }
    }
}
