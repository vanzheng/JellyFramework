using System;
using Jelly.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    [TestClass]
    public class StringUtilsTest
    {
        [TestMethod]
        public void ContainsWhiteSpaceTest()
        {
            string str = "ddd aaa";
            string str2 = "abc12d";

            bool result = StringUtils.ContainsWhiteSpace(str);
            bool result2 = StringUtils.ContainsWhiteSpace(str2);

            Assert.AreEqual(true, result);
            Assert.AreEqual(false, result2);
        }

        [TestMethod]
        public void IsAllWhiteSpaceTest() 
        {
            string a = string.Empty;
            string b = "a   b";
            string c = "    ";

            bool actual = StringUtils.IsAllWhiteSpace(a);
            bool actual2 = StringUtils.IsAllWhiteSpace(b);
            bool acutal3 = StringUtils.IsAllWhiteSpace(c);

            Assert.AreEqual(false, actual);
            Assert.AreEqual(false, actual2);
            Assert.AreEqual(true, acutal3);
        }

        [TestMethod]
        public void IsNullOrWhiteSpaceTest() 
        {
            string a = null;
            string b = string.Empty;
            string c = "  ";
            string d = "a  b";

            bool actual = StringUtils.IsNullOrWhiteSpace(a);
            bool actual2 = StringUtils.IsNullOrWhiteSpace(b);
            bool acutal3 = StringUtils.IsNullOrWhiteSpace(c);
            bool actual4 = StringUtils.IsNullOrWhiteSpace(d);

            Assert.AreEqual(true, actual);
            Assert.AreEqual(true, actual2);
            Assert.AreEqual(true, acutal3);
            Assert.AreEqual(false, actual4);
        }

        [TestMethod]
        public void IndentTest() 
        {
            char indentation = 'a';
            string input = "$abc123d";

            string actual = StringUtils.Indent(input, 5, indentation);
            string actual2 = StringUtils.Indent(input, 2);
            Assert.AreEqual("aaaaa$abc123d", actual);
            Assert.AreEqual("  $abc123d", actual2);
        }

        [TestMethod]
        public void TruncateTest() 
        {
            string a = "abcdefg123456789000";
            string b = "abcdefg";

            string actual = StringUtils.Truncate(a, 10, string.Empty);
            string actual2 = StringUtils.Truncate(a, 10);
            string actual3 = StringUtils.Truncate(b, 10, string.Empty);

            Assert.AreEqual("abcdefg123", actual);
            Assert.AreEqual("abcdefg123...", actual2);
            Assert.AreEqual("abcdefg", actual3);
        }

        [TestMethod]
        public void GetLineNumberTest() 
        {
            string content = string.Concat("a", Environment.NewLine, "b", Environment.NewLine, "c");
            int number = StringUtils.GetLineNumber(content);
            Assert.AreEqual(3, number);
        }

        [TestMethod]
        public void ToHexStringTest() 
        {
            byte[] a = new byte[] { 1, 2, 10 };
            byte[] b = new byte[] { 5, 6, 20, 12 };

            string actual = StringUtils.ToHexString(a);
            string expected = "01020A";
            string actual2 = StringUtils.ToHexString(b);
            string expected2 = "0506140C";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void RemoveSpecifiedIndexTest() 
        {
            string a = "abc1dddd$%";
            string result = StringUtils.RemoveSpecifiedIndex(a, 3);
            string expected = "abcdddd$%";
            Assert.AreEqual(expected, result);
        }

    }
}
