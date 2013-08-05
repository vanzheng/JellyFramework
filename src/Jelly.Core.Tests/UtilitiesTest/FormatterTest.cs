using Jelly.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jelly.Tests.UtilitiesTest
{
    [TestClass]
    public class FormatterTest
    {
        [TestMethod]
        public void FormatBytesTest()
        {
            int k0 = 1;
            int k1 = 1025;
            int k2 = 1024 * 1024 + 1000 * 100;
            int k3 = 1024 * 1024 * 1024 + 1000 * 100 * 1024;
            double k4 = 1024 * 1024 * 1024 * 1024d + 1000 * 100 * 1024 * 1024d;

            string result0 = Formatter.FormatBytes(k0);
            string result1 = Formatter.FormatBytes(k1);
            string result2 = Formatter.FormatBytes(k2);
            string result3 = Formatter.FormatBytes(k3);
            string result4 = Formatter.FormatBytes(k4);

            string result00 = Formatter.FormatBytes("#.##", k0);
            string result01 = Formatter.FormatBytes("#.##", k1);
            string result02 = Formatter.FormatBytes("#.##", k2);
            string result03 = Formatter.FormatBytes("#.##", k3);
            string result04 = Formatter.FormatBytes("#.##", k4);

            Assert.AreEqual("1B", result0);
            Assert.AreEqual("1KB", result1);
            Assert.AreEqual("1.1MB", result2);
            Assert.AreEqual("1.1GB", result3);
            Assert.AreEqual("1.1TB", result4);

            Assert.AreEqual("1B", result00);
            Assert.AreEqual("1KB", result01);
            Assert.AreEqual("1.1MB", result02);
            Assert.AreEqual("1.1GB", result03);
            Assert.AreEqual("1.1TB", result04);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FormatBytesNullParameterTest() 
        {
            object k = null;
            object k2 = new object();

            Formatter.FormatBytes(k);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FormatBytesNotSupportDataTypeTest()
        {
            object k = new object();
            Formatter.FormatBytes(k);
        }
    }
}
