using System;
using Jelly.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void ToComputerUnitTest()
        {
            ulong k0 = 1;
            ulong k1 = 1025;
            ulong k2 = 1024 * 1024 + 1000 * 100;
            ulong k3 = 1024 * 1024 * 1024 + 1000 * 100 * 1024;
            ulong k4 = 1024 * 1024 * 1024 * 1024L + 1000 * 100 * 1024 * 1024L;

            string result0 = Converter.ToComputerUnit(k0);
            string result1 = Converter.ToComputerUnit(k1);
            string result2 = Converter.ToComputerUnit(k2);
            string result3 = Converter.ToComputerUnit(k3);
            string result4 = Converter.ToComputerUnit(k4);

            string result00 = Converter.ToComputerUnit("#.##", k0);
            string result01 = Converter.ToComputerUnit("#.##", k1);
            string result02 = Converter.ToComputerUnit("#.##", k2);
            string result03 = Converter.ToComputerUnit("#.##", k3);
            string result04 = Converter.ToComputerUnit("#.##", k4);

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
    }
}
