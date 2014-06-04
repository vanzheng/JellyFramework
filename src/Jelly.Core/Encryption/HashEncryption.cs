using System;
using System.Security.Cryptography;
using System.Text;
using Jelly.Helpers;

namespace Jelly.Encryption
{
    /// <summary>
    /// Hash Encryption.
    /// </summary>
    public static class HashEncryption
    {
        /// <summary>
        /// Hash Encrypt a string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="mode">The <see cref="HashAlgorithmType"/> enum.</param>
        /// <returns>The encrypted hash string.</returns>
        public static string Encrypt(string input, HashAlgorithmType mode) 
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(input);

            byte[] utf8Bytes = Encoding.UTF8.GetBytes(input);            
            string result;

            switch (mode) 
            {
                case HashAlgorithmType.MD5:
                    result = CalculateHash(new MD5CryptoServiceProvider(), utf8Bytes);
                    break;
                case HashAlgorithmType.SHA1:
                    result = CalculateHash(new SHA1CryptoServiceProvider(), utf8Bytes);
                    break;
                case HashAlgorithmType.SHA256:
                    result = CalculateHash(new SHA256CryptoServiceProvider(), utf8Bytes);
                    break;
                case HashAlgorithmType.SHA384:
                    result = CalculateHash(new SHA384CryptoServiceProvider(), utf8Bytes);
                    break;
                case HashAlgorithmType.SHA512:
                    result = CalculateHash(new SHA512CryptoServiceProvider(), utf8Bytes);
                    break;
                default:
                    result = CalculateHash(new MD5CryptoServiceProvider(), utf8Bytes);
                    break;
            }

            return result;
        }

        private static string CalculateHash(HashAlgorithm algorithm, byte[] input) 
        {
            var data = algorithm.ComputeHash(input);
            return StringUtils.ToHexString(data);
        } 
    }

    /// <summary>
    /// The Hash Algorithm Type.
    /// </summary>
    public enum HashAlgorithmType 
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
}

