using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Jelly.Helpers;

namespace Jelly.Encryption
{
    /// <summary>
    /// The DES encryption.
    /// </summary>
    public static class DES
    {
        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="input">The encrypted string.</param>
        /// <param name="key">The secret key, must be 8 characters (64 bit).</param>
        /// <returns>The Base64 string.</returns>
        public static string Encrypt(string input, string key)
        {
            Validate(input, key);

            string result;

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }

                    result = Convert.ToBase64String(ms.ToArray());
                }
            }

            return result;
        }
        
        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="encryptedString">The encrypted string.</param>
        /// <param name="key">The secret key, must be 8 characters (64 bit).</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string encryptedString, string key)
        {
            Validate(encryptedString, key);

            string result;
            byte[] inputByteArray = Convert.FromBase64String(encryptedString);

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }

                    result = Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            return result;
        }

        private static void Validate(string input, string key) 
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(input);
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(key);
            ExceptionManager.ThrowArgumentExceptionIfMeet(key.Length != 8, "key", "The key must be 8 characters.");
        }
    }
}
