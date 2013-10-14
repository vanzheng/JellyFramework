using System;
using Jelly.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.UtilitiesTest
{
    /// <summary>
    /// Summary description for IOUtilityTest
    /// </summary>
    [TestClass]
    public class IOUtilityTest
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
            string actualFullPath = IOUtility.GetFullPath(path);
            string actualFullPath2 = IOUtility.GetFullPath(path2);
            string actualFullPath3 = IOUtility.GetFullPath(path3);
            string actualFullPath4 = IOUtility.GetFullPath(path4);

            Assert.AreEqual(expectedFullPath, actualFullPath);
            Assert.AreEqual(expectedFullPath2, actualFullPath2);
            Assert.AreEqual(expectedFullPath3, actualFullPath3);
            Assert.AreEqual(expectedFullPath4, actualFullPath4);
        }
    }
}
