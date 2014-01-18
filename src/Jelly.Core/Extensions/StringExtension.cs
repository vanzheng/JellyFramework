using System;
using System.Text.RegularExpressions;
using Jelly.Helpers;

namespace Jelly.Extensions
{
    /// <summary>
    /// The string extension class.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(this string SBCCase)
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
        public static string Truncate(this string s, int maximumLength)
        {
            return Truncate(s, maximumLength, "...");
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="s">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <param name="suffix">The suffix to place at the end of the truncated string.</param>
        /// <returns></returns>
        public static string Truncate(this string s, int maximumLength, string suffix)
        {
            if (suffix == null)
            {
                throw new ArgumentNullException("suffix");
            }

            if (maximumLength <= 0)
            {
                throw new ArgumentException("Maximum length must be greater than zero.", "maximumLength");
            }

            if (!string.IsNullOrWhiteSpace(s) && s.Length > maximumLength)
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
