using System;

namespace Jelly.Utilities
{
    public static class ConvertUtility
    {
        public static int? ToNullableInt32(object obj) 
        {
            if (obj == null) 
            {
                return null;
            }

            return Convert.ToInt32(obj);
        }
    }
}
