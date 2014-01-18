using System.Security.Cryptography;
using System.Text;

namespace Jelly.Encryption
{
    public class SHA1
    {
        public string Encrypt(string input)
        {
            return Encrypt(input, Encoding.UTF8);
        }

        public string Encrypt(string input, Encoding encoding) 
        {
            byte[] data = encoding.GetBytes(input);
            HashAlgorithm sha1 = new SHA1CryptoServiceProvider();
            data = sha1.ComputeHash(data);
            StringBuilder builder = new StringBuilder();
            foreach (byte b in data)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }
    }
}
