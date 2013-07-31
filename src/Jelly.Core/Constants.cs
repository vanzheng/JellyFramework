using System;

namespace Jelly
{
    public class Constants
    {
        public readonly static double Kilobyte = 1024d;
        public readonly static double Megabyte = Math.Pow(Kilobyte, 2);
        public readonly static double Gigabyte = Math.Pow(Kilobyte, 3);
        public readonly static double Terabyte = Math.Pow(Kilobyte, 4);
    }
}
