using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jelly.Utilities;

namespace Jelly.Tests.UtilitiesTest
{
    [TestClass]
    public class ArrayUtilityTest
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

            string[] result = ArrayUtility.Combin<string>(one, two);
            string[] result2 = ArrayUtility.Combin<string>(one2, two2);
            string[] result3 = ArrayUtility.Combin<string>(one3, two3);

            Assert.IsNull(result);
            Assert.IsNotNull(result2);
            Assert.AreEqual(1, result2.Length);
            Assert.IsNotNull(result3);
            Assert.AreEqual(3, result3.Length);
        }
    }
}
