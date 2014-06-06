using System;
using System.Globalization;

namespace Jelly.Helpers
{
    /// <summary>
    /// The converter utility.
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// Converts the data size to computer unit, like KB, MB, GB etc.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="data">The data size.</param>
        /// <returns>The computer unit string.</returns>
        public static string ToComputerUnit(string format, ulong data)
        {
            if (data >= 0 && data < Constants.Kilobyte)
            {
                return string.Concat(data.ToString("#"), "B");
            }
            else if (data >= Constants.Kilobyte && data < Constants.Megabyte)
            {
                return string.Concat((data / Constants.Kilobyte).ToString(format), "KB");
            }
            else if (data >= Constants.Megabyte && data < Constants.Gigabyte)
            {
                return string.Concat((data / Constants.Megabyte).ToString(format), "MB");
            }
            else if (data >= Constants.Gigabyte && data < Constants.Terabyte)
            {
                return string.Concat((data / Constants.Gigabyte).ToString(format), "GB");
            }
            else
            {
                return string.Concat((data / Constants.Terabyte).ToString(format), "TB");
            }
        }

        /// <summary>
        /// Converts the data size to computer unit, like KB, MB, GB etc.
        /// </summary>
        /// <param name="data">The data size.</param>
        /// <returns>The computer unit string.</returns>
        public static string ToComputerUnit(ulong data)
        {
            return ToComputerUnit("#.#", data);
        }
    }
}
