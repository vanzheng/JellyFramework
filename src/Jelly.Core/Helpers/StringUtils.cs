using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Jelly.Helpers
{
    /// <summary>
    /// The String Utility.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Determines whether the string contains white space.
        /// </summary>
        /// <param name="input">The string to test for white space.</param>
        /// <returns>
        /// 	<c>true</c> if the string contains white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsWhiteSpace(string input)
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(input, "input");

            foreach (char c in input) 
            {
                if (char.IsWhiteSpace(c))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the string is all white space. Empty string will return false.
        /// </summary>
        /// <param name="input">The string to test whether it is all white space.</param>
        /// <returns>
        /// 	<c>true</c> if the string is all white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAllWhiteSpace(string input)
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(input, "input");

            if (input.Length == 0)
            {
                return false;
            }

            foreach (char c in input) 
            {
                if (!char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the string is null or white space.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <returns>
        /// 	<c>true</c> if the SqlString is null or white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(string input)
        {
            if (input == null)
            {
                return true;
            }
            else
            {
                return input.Trim() == string.Empty ? true : false;
            }
        }

        /// <summary>
        /// Indents the specified string.
        /// </summary>
        /// <param name="input">The string to indent.</param>
        /// <param name="indentation">The number of characters to indent by.</param>
        /// <returns>The indented string.</returns>
        public static string Indent(string input, int indentation)
        {
            return Indent(input, indentation, ' ');
        }

        /// <summary>
        /// Indents the specified string.
        /// </summary>
        /// <param name="input">The string to indent.</param>
        /// <param name="indentation">The number of characters to indent by.</param>
        /// <param name="indentChar">The indent character.</param>
        /// <returns>The indented string.</returns>
        public static string Indent(string input, int indentation, char indentChar)
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(input, "input");
            ExceptionManager.ThrowArgumentExceptionIfMeet(indentation <= 0, "indentation", "Must be greater than zero.");

            string prefix = new String(indentChar, indentation);

            return string.Concat(prefix, input);
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="input">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <returns>The truncated string.</returns>
        public static string Truncate(string input, int maximumLength)
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
        public static string Truncate(string input, int maximumLength, string suffix)
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(input, "input");
            ExceptionManager.ThrowArgumentNullExceptionIfNull(suffix, "suffix");
            ExceptionManager.ThrowArgumentExceptionIfMeet(maximumLength <= 0, "maximumLength", "Maximum length must be greater than zero.");

            if (!IsNullOrWhiteSpace(input) && input.Length > maximumLength)
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
        /// Get the content count line number.
        /// </summary>
        /// <param name="content">The content string.</param>
        /// <returns>The count line number.</returns>
        public static int GetLineNumber(string content) 
        {
            ExceptionManager.ThrowIfNull<ArgumentNullException>(content);

            int total = 1;
            Regex regex = new Regex(@"\r\n", RegexOptions.Compiled | RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(content);

            foreach (Match match in matches) 
            {
                if (match.Success) 
                {
                    total++;
                }
            }

            return total;
        }

        /// <summary>
        /// Byte array convert to hexadecimal string.
        /// </summary>
        /// <param name="byteInput">The input byte array.</param>
        /// <returns>The hex string.</returns>
        public static string ToHexString(byte[] byteInput) 
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(byteInput);

            int len = byteInput.Length;
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                builder.Append(byteInput[i].ToString("X2"));
            }

            return builder.ToString();
        }
    }
}
