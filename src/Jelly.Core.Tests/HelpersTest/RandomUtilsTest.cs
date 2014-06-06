using System;
using Jelly.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    [TestClass]
    public class RandomUtilsTest
    {
        [TestMethod]
        public void CreateRandomTest()
        {
            var rnd = RandomUtils.CreateRandom();
            var rnd2 = RandomUtils.CreateRandom();
            int next = rnd.Next();
            int next2 = rnd2.Next();
            Assert.AreNotEqual(next, next2);
        }

        [TestMethod]
        public void GetRandomNumberTest() 
        {
            int next = RandomUtils.GetRandomNumber(1000);
            int next2 = RandomUtils.GetRandomNumber(1000);
            Assert.AreNotEqual(next, next2);
        }

        [TestMethod]
        public void GetRandomNumberAtRangeTest() 
        {
            int next = RandomUtils.GetRandomNumber(1000, 10000);
            int next2 = RandomUtils.GetRandomNumber(1000, 10000);
            Assert.AreNotEqual(next, next2);
        }
    }
}
