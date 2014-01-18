using Jelly.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    [TestClass]
    public class ArrayUtilsTest
    {
        [TestMethod]
        public void CombinTest()
        {
            string[] one = null;
            string[] two = null;
            string[] one2 = null;
            string[] two2 = new string[] { "1" };
            string[] one3 = new string[] { "1", "2" };
            string[] two3 = new string[] { "3" };

            string[] result = ArrayUtils.Combin<string>(one, two);
            string[] result2 = ArrayUtils.Combin<string>(one2, two2);
            string[] result3 = ArrayUtils.Combin<string>(one3, two3);

            Assert.IsNull(result);
            Assert.IsNotNull(result2);
            Assert.AreEqual(1, result2.Length);
            Assert.IsNotNull(result3);
            Assert.AreEqual(3, result3.Length);
        }
    }
}
