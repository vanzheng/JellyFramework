using System;
using System.Globalization;

namespace Jelly.Helpers
{
    public class Formatter
    {
        /// <summary>
        /// Formats bytes string.
        /// </summary>
        public static string FormatBytes(string format, object bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            if (bytes is Byte ||
                bytes is SByte ||
                bytes is Int16 ||
                bytes is UInt16 ||
                bytes is Int32 ||
                bytes is UInt32 ||
                bytes is Int64 ||
                bytes is UInt64 ||
                bytes is Single ||
                bytes is Double)
            {

                double size = Convert.ToDouble(bytes, CultureInfo.CurrentCulture);

                if (size < 0)
                {
                    throw new Exception("The byte size less than 0, can not format.");
                }

                if (size >= 0 && size < Constants.Kilobyte)
                {
                    return string.Concat(size.ToString("#"), "B");
                }
                else if (size >= Constants.Kilobyte && size < Constants.Megabyte)
                {
                    return string.Concat((size / Constants.Kilobyte).ToString(format), "KB");
                }
                else if (size >= Constants.Megabyte && size < Constants.Gigabyte)
                {
                    return string.Concat((size / Constants.Megabyte).ToString(format), "MB");
                }
                else if (size >= Constants.Gigabyte && size < Constants.Terabyte)
                {
                    return string.Concat((size / Constants.Gigabyte).ToString(format), "GB");
                }
                else
                {
                    return string.Concat((size / Constants.Terabyte).ToString(format), "TB");
                }
            }
            else
            {
                throw new Exception("The given arg data type is not Byte, sbyte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Single, Double");
            }
        }

        public static string FormatBytes(object bytes)
        {
            return FormatBytes("#.#", bytes);
        }
    }
}
