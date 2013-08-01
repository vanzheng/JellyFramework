using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Jelly.Utilities;

namespace Jelly.Extensions
{
    /// <summary>
    /// The string extension class.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Formats bytes string.
        /// </summary>
        public static string FormatBytes(this string str, string formatter, object bytes)
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
                bytes is UInt64)
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
                    return string.Concat((size / Constants.Kilobyte).ToString("#"), "KB");
                }
                else if (size >= Constants.Megabyte && size < Constants.Gigabyte)
                {
                    return string.Concat((size / Constants.Megabyte).ToString(formatter), "MB");
                }
                else if (size >= Constants.Gigabyte && size < Constants.Terabyte)
                {
                    return string.Concat((size / Constants.Gigabyte).ToString(formatter), "GB");
                }
                else
                {
                    return string.Concat((size / Constants.Terabyte).ToString(formatter), "TB");
                }
            }
            else 
            {
                throw new Exception("The given arg data type is not Byte, sbyte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64");
            }
        }

        public static string FormatBytes(this string str, int bytes) 
        {
            return FormatBytes(str, "#.#", bytes);
        }

        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(this string str, string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="s">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <returns></returns>
        public static string Truncate(this string str, string s, int maximumLength)
        {
            return Truncate(str, s, maximumLength, "...");
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="s">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <param name="suffix">The suffix to place at the end of the truncated string.</param>
        /// <returns></returns>
        public static string Truncate(this string str, string s, int maximumLength, string suffix)
        {
            if (suffix == null)
            {
                throw new ArgumentNullException("suffix");
            }

            if (maximumLength <= 0)
            {
                throw new ArgumentException("Maximum length must be greater than zero.", "maximumLength");
            }

            if (!string.IsNullOrEmpty(s) && s.Length > maximumLength)
            {
                string truncatedString = s.Substring(0, maximumLength);
                // incase the last character is a space
                truncatedString = truncatedString.TrimEnd();
                truncatedString += suffix;

                return truncatedString;
            }
            else
            {
                return s;
            }
        }

        public static bool IsMatch(this string str, string pattern) 
        {
            if (str == null) 
            {
                return false;
            }

            Regex regex = new Regex(pattern);
            return regex.IsMatch(str);
        }

        public static bool IsMatch(this string str, string pattern, RegexOptions regexOptions) 
        {
            if (str == null)
            {
                return false;
            }

            Regex regex = new Regex(pattern, regexOptions);
            return regex.IsMatch(str);
        }

        public static string ReplaceArray(this string str, char[] oldChar, char newChar) 
        {
            if (str == null) 
            {
                return null;
            }

            if (oldChar == null || oldChar.Length == 0) 
            {
                return str;
            }

            int len = oldChar.Length;
            for (int i = 0; i < len; i++) 
            {
                str.Replace(oldChar[i], newChar);
            }

            return str;
        }

        public static string ReplaceArray(this string str, string[] oldString, string newString)
        {
            if (str == null)
            {
                return null;
            }

            if (oldString == null || oldString.Length == 0 || newString == null)
            {
                return str;
            }

            int len = oldString.Length;
            for (int i = 0; i < len; i++)
            {
                str.Replace(oldString[i], newString);
            }

            return str;
        }

        public static bool IsLastWord(this string input, string lastWord)
        {
            ExceptionManager.ThrowIfNull(input, "input");
            ExceptionManager.ThrowIfNull(lastWord, "lastWord");

            int wordLength = lastWord.Length;
            return input.LastIndexOf(lastWord) == input.Length - wordLength;
        }

        public static bool IsLastWord(this string input, char lastWord)
        {
            ExceptionManager.ThrowIfNull(input, "input");
            ExceptionManager.ThrowIfNull(lastWord, "lastWord");

            return input.LastIndexOf(lastWord) == input.Length - 1;
        }
    }
}
