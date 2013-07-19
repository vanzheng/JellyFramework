using System;
using System.Data.Common;

namespace Jelly.Extensions
{
    public static class DbDataReaderExtension
    {
        public static T GetValue<T>(this DbDataReader reader, int i)
        {
            object result = reader.GetValue(i);
            return result == DBNull.Value ? default(T) : (T)result;
        }

        public static T GetValue<T>(this DbDataReader reader, string name) 
        {
            int index = reader.GetOrdinal(name);
            return GetValue<T>(reader, index);
        }
    }
}
