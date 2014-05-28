using Jelly.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.EncryptionTest
{
    [TestClass]
    public class Base64Test
    {
        [TestMethod]
        public void EncryptTest()
        {
            string str = "abc123";
            string actual = Base64.Encrypt(str);
            Assert.AreEqual("YWJjMTIz", actual);
        }

        [TestMethod]
        public void DecryptTest() 
        {
            string str = "YWJjMTIz";
            string actual = Base64.Decrypt(str);
            Assert.AreEqual("abc123", actual);
        }
    }
}
