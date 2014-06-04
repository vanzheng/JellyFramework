using System;
using System.Data.Common;
using Jelly.Helpers;

namespace Jelly.Extensions
{
    /// <summary>
    /// The DbDataReader extension.
    /// </summary>
    public static class DbDataReaderExtension
    {
        /// <summary>
        /// Gets the generic value by field index.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="reader">The <see cref="DbDataReader"/> object.</param>
        /// <param name="index">The field index in DbDataReader.</param>
        /// <returns>The generic value.</returns>
        public static T GetValue<T>(this DbDataReader reader, int index)
        {
            object result = reader.GetValue(index);
            return result == DBNull.Value ? default(T) : (T)result;
        }

        /// <summary>
        /// Gets the generic value by field name.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="reader">The <see cref="DbDataReader"/> object.</param>
        /// <param name="name">The field name in DbDataReader.</param>
        /// <returns>The generic value.</returns>
        public static T GetValue<T>(this DbDataReader reader, string name) 
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(name);

            int index = reader.GetOrdinal(name);
            return GetValue<T>(reader, index);
        }
    }
}
