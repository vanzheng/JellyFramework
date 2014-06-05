using System;
using Jelly.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    [TestClass]
    public class RMBUtilsTest
    {
        [TestMethod]
        public void ToCapitalCNYTest()
        {
            decimal zero = 0.000M;
            decimal number = 1826578450.62M;
            decimal number2 = 1002006M;
            decimal number3 = 0.001M;
            decimal number4 = 1.05M;

            string zeroCapital = RMBUtils.ToCapitalCNY(zero);
            string numberCapital = RMBUtils.ToCapitalCNY(number);
            string number2Capital = RMBUtils.ToCapitalCNY(number2);
            string number3Capital = RMBUtils.ToCapitalCNY(number3);
            string number4Capital = RMBUtils.ToCapitalCNY(number4);

            string zeroExpected = "零元整";
            string numberCapitalExpected = "壹拾捌亿贰仟陆佰伍拾柒万捌仟肆佰伍拾元陆角贰分";
            string number2CapitalExpected = "壹佰万贰仟零陆元整";
            string number3CapitalExpected = "零元整";
            string number4CapitalExpected = "壹元零伍分";

            Assert.AreEqual(zeroExpected, zeroCapital);
            Assert.AreEqual(numberCapitalExpected, numberCapital);
            Assert.AreEqual(number2CapitalExpected, number2Capital);
            Assert.AreEqual(number3CapitalExpected, number3Capital);
            Assert.AreEqual(number4CapitalExpected, number4Capital);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToCapitalCNYOverflowTest() 
        {
            decimal d = 19999999999999.98M;
            RMBUtils.ToCapitalCNY(d);
        }
    }
}
