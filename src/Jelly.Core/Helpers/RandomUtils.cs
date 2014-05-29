using System;
using System.Security.Cryptography;

namespace Jelly.Helpers
{
    /// <summary>
    /// The Random helper.
    /// </summary>
    public static class RandomUtils
    {
        /// <summary>
        /// Creates random number.
        /// </summary>
        /// <returns>The random number.</returns>
        public static Random CreateRandom() 
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] byteSeed = new byte[4];
            rng.GetBytes(byteSeed);
            int seed = Math.Abs(BitConverter.ToInt32(byteSeed, 0));
            return new Random(seed);
        }

        /// <summary>
        /// Creates random number within maximum.
        /// </summary>
        /// <param name="max">The maximum number.</param>
        /// <returns>The random number.</returns>
        public static int GetRandomNumber(int max) 
        {
            Random random = CreateRandom();
            return random.Next(max);
        }

        /// <summary>
        /// Creates random number between minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum number.</param>
        /// <param name="max">The maximum number.</param>
        /// <returns>The random number.</returns>
        public static int GetRandomNumber(int min, int max)
        {
            Random random = CreateRandom();
            return random.Next(min, max);
        }
    }
}
