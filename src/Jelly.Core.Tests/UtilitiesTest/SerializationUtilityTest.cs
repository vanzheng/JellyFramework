using System;
using Jelly.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.UtilitiesTest
{
    [TestClass]
    public class SerializationUtilityTest
    {
        [TestMethod]
        public void DeepCopyTest()
        {
            Person person = new Person()
            {
                Name = "A",
                Age = 20
            };

            Family family = new Family()
            {
                FamilyName = "Big A",
                Population = 10
            };

            person.Family = family;
            Person person2 = SerializationUtility.DeepCopy<Person>(person);
            family.Population = 8;

            Assert.AreEqual("A", person2.Name);
            Assert.AreEqual(20, person2.Age);
            Assert.AreEqual("Big A", person2.Family.FamilyName);
            Assert.AreEqual(10, person2.Family.Population);
            Assert.AreEqual(8, person.Family.Population);
        }

        [Serializable]
        private class Person 
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Family Family { get; set; }
        }

        [Serializable]
        private class Family 
        {
            public string FamilyName { get; set; }
            public int Population { get; set; }
        }
    }
}
