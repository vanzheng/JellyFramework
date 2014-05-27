using System;
using System.Text;

namespace Jelly.Encryption
{
    /// <summary>
    /// Base64 Encryption
    /// </summary>
    public static class Base64
    {
        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="utf8Str">The utf-8 string.</param>
        /// <returns>The Base64 string.</returns>
        public static string Decrypt(string base64Str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64Str));
        }

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="utf8Str">The utf-8 string.</param>
        /// <returns>The Base64 string.</returns>
        public static string Encrypt(string utf8Str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(utf8Str));
        }
    }
}
