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
            ExceptionManager.ThrowArgumentNullExceptionIfNull(suffix, "suffix");
            ExceptionManager.ThrowArgumentExceptionIfMeet(maximumLength <= 0, "maximumLength", "Maximum length must be greater than zero.");

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
        /// Replace old character array to new character arry.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="oldChar">The old character array.</param>
        /// <param name="newChar">The new character arry.</param>
        /// <returns>The replaced string.</returns>
        public static string Replace(this string input, char[] oldChar, char newChar) 
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

        /// <summary>
        /// Replace old string array to new string arry.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="oldChar">The old string array.</param>
        /// <param name="newChar">The new string arry.</param>
        /// <returns>The replaced string.</returns>
        public static string Replace(this string input, string[] oldString, string newString)
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

        /// <summary>
        /// Identify the whether the input parameter is last word.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="lastWord">The last word string.</param>
        /// <returns>
        ///     <c>true</c>, the input parameter is match last word, otherwise <c>false</c>.
        /// </returns>
        public static bool IsLastWord(this string input, string lastWord)
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(input, "input");
            ExceptionManager.ThrowArgumentNullExceptionIfNull(lastWord, "lastWord");

            int wordLength = lastWord.Length;
            return input.LastIndexOf(lastWord) == input.Length - wordLength;
        }

        /// <summary>
        /// Identify the whether the input parameter is last word.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="lastWord">The last word char.</param>
        /// <returns>
        ///     <c>true</c>, the input parameter is match last word, otherwise <c>false</c>.
        /// </returns>
        public static bool IsLastWord(this string input, char lastWord)
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(input, "input");
            ExceptionManager.ThrowArgumentNullExceptionIfNull(lastWord, "lastWord");

            return input.LastIndexOf(lastWord) == input.Length - 1;
        }
    }
}
