using Jelly.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.EncryptionTest
{
    [TestClass]
    public class LegacyMD5Test
    {
        [TestMethod]
        public void EncryptTest()
        {
            string md5 = "test@md512";

            string md5Result = LegacyMD5.Encrypt(md5, MD5Length.ThirtyTwo);
            string md5Expected = "41d2a95c9dc0149f78c1f3ed0e473c3c";
            string md5Result2 = LegacyMD5.Encrypt(md5, MD5Length.Sixteen);
            string md5Expected2 = "9dc0149f78c1f3ed";

            Assert.AreEqual(md5Expected, md5Result);
            Assert.AreEqual(md5Expected2, md5Result2);
        }
    }
}
