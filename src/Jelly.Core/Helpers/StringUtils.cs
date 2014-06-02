using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Jelly.Helpers
{
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
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
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
        public static bool IsWhiteSpace(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsWhiteSpace(input[i]))
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
        /// <returns></returns>
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
        /// <returns></returns>
        public static string Indent(string input, int indentation, char indentChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (indentation <= 0)
            {
                throw new ArgumentException("Must be greater than zero.", "indentation");
            }

            string prefix = new String(indentChar, indentation);

            return string.Concat(prefix, input);
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="input">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <returns></returns>
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
        /// <returns></returns>
        public static string Truncate(string input, int maximumLength, string suffix)
        {
            if (string.IsNullOrEmpty(input)) 
            {
                throw new ArgumentNullException("input");
            }
            
            if (suffix == null)
            {
                throw new ArgumentNullException("suffix");
            }

            if (maximumLength <= 0)
            {
                throw new ArgumentException("Maximum length must be greater than zero.", "maximumLength");
            }

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
        /// Byte array conver to hexadecimal string.
        /// </summary>
        /// <param name="byteInput"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] byteInput) 
        {
            ExceptionManager.ThrowIfNull(byteInput);

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
