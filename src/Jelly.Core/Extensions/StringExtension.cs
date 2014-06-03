using System;
using System.Text;
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
        /// Convert SBC case to half-pitch string.
        /// </summary>
        /// <param name="sbcCase">The SBC case string.</param>
        /// <returns>The half-pitch string.</returns>
        public static string SBCCaseToNumberic(this string sbcCase)
        {
            char[] c = sbcCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="input">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <returns>The truncated string.</returns>
        public static string Truncate(this string input, int maximumLength)
        {
            return Truncate(input, maximumLength, "...");
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="input">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <param name="suffix">The suffix to place at the end of the truncated string.</param>
        /// <returns>The truncated string.</returns>
        public static string Truncate(this string input, int maximumLength, string suffix)
        {
            if (suffix == null)
            {
                throw new ArgumentNullException("suffix");
            }

            if (maximumLength <= 0)
            {
                throw new ArgumentException("Maximum length must be greater than zero.", "maximumLength");
            }

            if (!string.IsNullOrWhiteSpace(input) && input.Length > maximumLength)
            {
                string truncatedString = input.Substring(0, maximumLength);
                // incase the last character is a space
                truncatedString = truncatedString.TrimEnd();
                truncatedString += suffix;

                return truncatedString;
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// The string is match with the regex pattern.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="pattern">The regex pattern.</param>
        /// <returns>
        ///     <c>true</c> if the string match the pattern; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMatch(this string input, string pattern) 
        {
            if (input == null) 
            {
                return false;
            }

            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// The string is match with the regex pattern.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="pattern">The regex pattern.</param>
        /// <param name="regexOptions">The <see cref="RegexOptions"/> enum.</param>
        /// <returns>
        ///     <c>true</c> if the string match the pattern; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMatch(this string input, string pattern, RegexOptions regexOptions) 
        {
            if (input == null)
            {
                return false;
            }

            Regex regex = new Regex(pattern, regexOptions);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Replace old character to new character.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oldChar"></param>
        /// <param name="newChar"></param>
        /// <returns></returns>
        public static string ReplaceArray(this string input, char[] oldChar, char newChar) 
        {
            if (input == null) 
            {
                return null;
            }

            if (oldChar == null || oldChar.Length == 0) 
            {
                return input;
            }

            int len = oldChar.Length;
            for (int i = 0; i < len; i++) 
            {
                input.Replace(oldChar[i], newChar);
            }

            return input;
        }

        public static string ReplaceArray(this string input, string[] oldString, string newString)
        {
            if (input == null)
            {
                return null;
            }

            if (oldString == null || oldString.Length == 0 || newString == null)
            {
                return input;
            }

            int len = oldString.Length;
            for (int i = 0; i < len; i++)
            {
                input.Replace(oldString[i], newString);
            }

            return input;
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
