using System;

namespace Jelly.Utilities
{
    public static class ArrayUtility
    {
        public static T[] Combin<T>(T[] oneArray, T[] twoArray) 
        {
            if (oneArray == null && twoArray == null) 
            {
                return null;
            }

            if (oneArray == null) 
            {
                return twoArray;
            }

            if (twoArray == null) 
            {
                return oneArray;
            }

            int len = oneArray.Length,
                len2 = twoArray.Length;

            T[] result = new T[len + len2];
            Array.Copy(oneArray, result, len);
            Array.Copy(twoArray, 0, result, len - 1, len2);
            return result;
        }
    }
}
