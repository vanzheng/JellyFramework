using System;
using Jelly.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    /// <summary>
    /// Summary description for IOUtilsTest
    /// </summary>
    [TestClass]
    public class IOUtilsTest
    {
        [TestMethod]
        public void GetFullPathTest()
        {
            string path = "/test";
            string path2 = "~/test";
            string path3 = "/";
            string path4 = "~/";
            string expectedFullPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + path.Substring(1).Replace("/", "\\");
            string expectedFullPath2 = AppDomain.CurrentDomain.BaseDirectory + "\\" + path2.Substring(2).Replace("/", "\\");
            string expectedFullPath3 = AppDomain.CurrentDomain.BaseDirectory + "\\";
            string expectedFullPath4 = expectedFullPath3;
            string actualFullPath = IOUtils.GetFullPath(path);
            string actualFullPath2 = IOUtils.GetFullPath(path2);
            string actualFullPath3 = IOUtils.GetFullPath(path3);
            string actualFullPath4 = IOUtils.GetFullPath(path4);

            Assert.AreEqual(expectedFullPath, actualFullPath);
            Assert.AreEqual(expectedFullPath2, actualFullPath2);
            Assert.AreEqual(expectedFullPath3, actualFullPath3);
            Assert.AreEqual(expectedFullPath4, actualFullPath4);
        }
    }
}
