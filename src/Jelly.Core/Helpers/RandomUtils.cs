using System;
using System.Security.Cryptography;

namespace Jelly.Helpers
{
    public static class RandomUtils
    {
        public static Random CreateRandom() 
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] byteSeed = new byte[4];
            rng.GetBytes(byteSeed);
            int seed = Math.Abs(BitConverter.ToInt32(byteSeed, 0));
            return new Random(seed);
        }

        public static int GetRandomNumber(int max) 
        {
            Random random = CreateRandom();
            return random.Next(max);
        }

        public static int GetRandomNumber(int min, int max)
        {
            Random random = CreateRandom();
            return random.Next(min, max);
        }
    }
}
