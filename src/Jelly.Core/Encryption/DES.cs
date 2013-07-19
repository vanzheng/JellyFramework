using System;
using System.Security.Cryptography;
using System.Text;

namespace Jelly.Encryption
{
    public class DES
    {
        #region DESEncrypt DES����
        /// <summary>
        /// ����DES���ܡ�
        /// </summary>
        /// <param name="pToEncrypt">Ҫ���ܵ��ַ�����</param>
        /// <param name="sKey">��Կ���ұ���Ϊ8λ��</param>
        /// <returns>��Base64��ʽ���صļ����ַ�����</returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion

        #region DESDecrypt DES����
        /// <summary>
        /// ����DES���ܡ�
        /// </summary>
        /// <param name="pToDecrypt">Ҫ���ܵ���Base64</param>
        /// <param name="sKey">��Կ���ұ���Ϊ8λ��</param>
        /// <returns>�ѽ��ܵ��ַ�����</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion
    }
}