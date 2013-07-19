using System;
using System.Text;

namespace Jelly.Encryption
{
    /// <summary>
    /// Base64 Encryption
    /// </summary>
    public class Base64
    {

        public static string Decrypt(string base64Str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64Str));
        }

        public static string Encrypt(string utf8Str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(utf8Str));
        }
    }
}
