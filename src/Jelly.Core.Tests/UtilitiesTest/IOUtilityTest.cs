using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jelly.Utilities;

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
            string path = "/bin/Debug";
            string path2 = "~/bin/Debug";
            string expectedFullPath = AppDomain.CurrentDomain.BaseDirectory + path.Substring(2).Replace("/", "\\");
            string expectedFullPath2 = AppDomain.CurrentDomain.BaseDirectory + path2.Substring(2).Replace("/", "\\");
            string actualFullPath = IOUtility.GetFullPath(path);
            string actualFullPath2 = IOUtility.GetFullPath(path2);
            Assert.AreEqual(expectedFullPath, actualFullPath);
            Assert.AreEqual(expectedFullPath2, actualFullPath2);
        }
    }
}
