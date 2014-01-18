using System;

namespace Jelly
{
    internal static class Constants
    {
        public const string CarriageReturnLineFeed = "\r\n";
        public const char CarriageReturn = '\r';
        public const char LineFeed = '\n';

        public readonly static double Kilobyte = 1024d;
        public readonly static double Megabyte = Math.Pow(Kilobyte, 2);
        public readonly static double Gigabyte = Math.Pow(Kilobyte, 3);
        public readonly static double Terabyte = Math.Pow(Kilobyte, 4);
    }
}
