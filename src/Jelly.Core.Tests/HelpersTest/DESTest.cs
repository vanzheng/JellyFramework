using System;
using Jelly.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    [TestClass]
    public class DESTest
    {
        [TestMethod]
        public void EncryptionTest()
        {
            string input = "This is a des string.";
            string key = "adc#98er";

            string encryptedString = DES.Encrypt(input, key);
            string result = DES.Decrypt(encryptedString, key);
            Assert.AreEqual(input, result);
        }
    }
}
