using System;
using Jelly.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    [TestClass]
    public class ExceptionManagerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionForObjectWithParameterNameAndMessageTest()
        {
            object obj = null;
            ExceptionManager.ThrowArgumentNullExceptionIfNull(obj, "obj", "throw a ArgumentNullException");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionForObjectWithParameterNameTest()
        {
            object obj = null;
            ExceptionManager.ThrowArgumentNullExceptionIfNull(obj, "obj");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionForObjectTest()
        {
            object obj = null;
            ExceptionManager.ThrowArgumentNullExceptionIfNull(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ThrowExceptionForGenericTest()
        {
            object obj = null;
            ExceptionManager.ThrowIfNull<Exception>(obj);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ThrowExceptionForGenericConditionTest()
        {
            bool condition = true;
            ExceptionManager.ThrowIfNull<Exception>(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgumentExceptionForMeetWithParameterNameAndMessageTest()
        {
            bool condition = true;
            ExceptionManager.ThrowArgumentExceptionIfMeet(condition, "condition", "throw a ArgumentException");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgumentExceptionForMeetWithMessageTest()
        {
            bool condition = true;
            ExceptionManager.ThrowArgumentExceptionIfMeet(condition, "throw a ArgumentException");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgumentExceptionForMeetTest()
        {
            bool condition = true;
            ExceptionManager.ThrowArgumentExceptionIfMeet(condition);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionForStringWithParameterNameAndMessageTest()
        {
            string str = string.Empty;
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(str, "str", "throw a ArgumentNullException");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionForStringWithParameterNameTest()
        {
            string str = string.Empty;
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(str, "str");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionForStringTest()
        {
            string str = string.Empty;
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(str);
        }
    }
}
