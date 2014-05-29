using System;

namespace Jelly.Helpers
{
    /// <summary>
    /// The Array helper.
    /// </summary>
    public static class ArrayUtils
    {
        /// <summary>
        /// Combin tow array into one array.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="oneArray">The first array.</param>
        /// <param name="twoArray">The second array.</param>
        /// <returns>The combined array.</returns>
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
