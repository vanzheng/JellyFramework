using System;
using System.Security.Cryptography;
using System.Text;
using Jelly.Helpers;

namespace Jelly.Encryption
{
    /// <summary>
    /// Hash Encryption.
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// Hash Encrypt a string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="mode">The <see cref="EncryptionHashMode"/> enum.</param>
        /// <returns>The encrypted hash string.</returns>
        public static string Encrypt(string input, EncryptionHashMode mode) 
        {
            ExceptionManager.ThrowIfNullOrEmpty(input);

            byte[] utf8Bytes = Encoding.UTF8.GetBytes(input);            
            string result;

            switch (mode) 
            {
                case EncryptionHashMode.MD5:
                    result = CalculateHash(new MD5CryptoServiceProvider(), utf8Bytes);
                    break;
                case EncryptionHashMode.SHA1:
                    result = CalculateHash(new SHA1CryptoServiceProvider(), utf8Bytes);
                    break;
                case EncryptionHashMode.SHA256:
                    result = CalculateHash(new SHA256CryptoServiceProvider(), utf8Bytes);
                    break;
                case EncryptionHashMode.SHA384:
                    result = CalculateHash(new SHA384CryptoServiceProvider(), utf8Bytes);
                    break;
                case EncryptionHashMode.SHA512:
                    result = CalculateHash(new SHA512CryptoServiceProvider(), utf8Bytes);
                    break;
                default:
                    result = CalculateHash(new MD5CryptoServiceProvider(), utf8Bytes);
                    break;
            }

            return result;
        }

        public static string CalculateHash(HashAlgorithm algorithm, byte[] input) 
        {
            var data = algorithm.ComputeHash(input);
            return StringUtils.ToHexString(data);
        } 
    }

    public enum EncryptionHashMode 
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
}

