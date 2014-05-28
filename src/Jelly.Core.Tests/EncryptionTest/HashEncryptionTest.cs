using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jelly.Encryption;

namespace Jelly.Tests.EncryptionTest
{
    [TestClass]
    public class HashEncryptionTest
    {
        [TestMethod]
        public void EncryptionTest()
        {
            string md5 = "test@md512";
            string sha1 = "acbcsh%ee";
            string sha256 = "afesj2#2e3";
            string sha384 = "jiefwan2";
            string sha512 = "rufn34^8d3";

            string md5Result = HashEncryption.Encrypt(md5, HashAlgorithmType.MD5);
            string md5Expected = "41D2A95C9DC0149F78C1F3ED0E473C3C";

            string sha1Result = HashEncryption.Encrypt(sha1, HashAlgorithmType.SHA1);
            string sha1Expected = "DD6B9261B5E69D7F3380A019F6AB645C08DB41D1";

            string sha256Result = HashEncryption.Encrypt(sha256, HashAlgorithmType.SHA256);
            string sha256Expected = "B051CCA51B08C688CA6B892BE9458DD8C90429392EC111E2A7AF9491AF466415";

            string sha384Result = HashEncryption.Encrypt(sha384, HashAlgorithmType.SHA384);
            string sha384Expected = "9B7C6BFDE65BA8059435C8CC4E19E85EF04F9DDB2156AE8DE6CD9BEC3116BECF69BD4FB37163FD06F7FF2FB98FDD4420";

            string sha512Result = HashEncryption.Encrypt(sha512, HashAlgorithmType.SHA512);
            string sha512Expected = "A424F78A0442C767EFC13243DE46F7AF990E0F3DE21B4A0C22AB3DD627F502088C42C2238AC29B38B04FF9EF64097CC29F52078432E9978FC53EB896876175A0";

            Assert.AreEqual(md5Expected, md5Result);
            Assert.AreEqual(sha1Expected, sha1Result);
            Assert.AreEqual(sha256Expected, sha256Result);
            Assert.AreEqual(sha384Expected, sha384Result);
            Assert.AreEqual(sha512Expected, sha512Result);
        }
    }
}
