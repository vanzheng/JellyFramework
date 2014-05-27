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
        /// <param name="s">The string to test for white space.</param>
        /// <returns>
        /// 	<c>true</c> if the string contains white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsWhiteSpace(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsWhiteSpace(s[i]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the string is all white space. Empty string will return false.
        /// </summary>
        /// <param name="s">The string to test whether it is all white space.</param>
        /// <returns>
        /// 	<c>true</c> if the string is all white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWhiteSpace(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (s.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsWhiteSpace(s[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the string is null or white space.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>
        /// 	<c>true</c> if the SqlString is null or white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(string s)
        {
            if (s == null)
            {
                return true;
            }
            else
            {
                return s.Trim() == string.Empty ? true : false;
            }
        }

        /// <summary>
        /// Indents the specified string.
        /// </summary>
        /// <param name="s">The string to indent.</param>
        /// <param name="indentation">The number of characters to indent by.</param>
        /// <returns></returns>
        public static string Indent(string s, int indentation)
        {
            return Indent(s, indentation, ' ');
        }

        /// <summary>
        /// Indents the specified string.
        /// </summary>
        /// <param name="s">The string to indent.</param>
        /// <param name="indentation">The number of characters to indent by.</param>
        /// <param name="indentChar">The indent character.</param>
        /// <returns></returns>
        public static string Indent(string s, int indentation, char indentChar)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (indentation <= 0)
            {
                throw new ArgumentException("Must be greater than zero.", "indentation");
            }

            string prefix = new String(indentChar, indentation);

            return string.Concat(prefix, s);
        }

        /// <summary>
        /// Truncates the specified string.
        /// </summary>
        /// <param name="s">The string to truncate.</param>
        /// <param name="maximumLength">The maximum length of the string before it is truncated.</param>
        /// <returns></returns>
        public static string Truncate(string s, int maximumLength)
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
        public static string Truncate(string s, int maximumLength, string suffix)
        {
            if (suffix == null)
            {
                throw new ArgumentNullException("suffix");
            }

            if (maximumLength <= 0)
            {
                throw new ArgumentException("Maximum length must be greater than zero.", "maximumLength");
            }

            if (!IsNullOrWhiteSpace(s) && s.Length > maximumLength)
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

        public static int GetLineNumber(string s) 
        {
            ExceptionManager.ThrowIfNull<ArgumentNullException>(s);

            int total = 1;
            Regex regex = new Regex(@"\r\n", RegexOptions.Compiled | RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(s);

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
