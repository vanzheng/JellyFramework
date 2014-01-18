using System;
using System.IO;
using Jelly.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.HelpersTest
{
    [TestClass]
    public class SerializationUtilsTest
    {
        [TestMethod]
        public void XmlFileToObjectTest() 
        {
            string xmlPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\_data\", "Car.xml");
            Car car = SerializationUtils.XmlToObject<Car>(xmlPath);

            Assert.IsNotNull(car);
            Assert.AreEqual("BMW", car.Name);
            Assert.AreEqual(4, car.Wheel);
        }

        [TestMethod]
        public void ObjectToXmlFileTest() 
        {
            string xmlPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\_data\", "SavedCar.xml");
            Car car = new Car()
            {
                Name = "A",
                Wheel = 4
            };

            SerializationUtils.ObjectToXml(xmlPath, car);
            Assert.IsTrue(File.Exists(xmlPath));
        }

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
            Person person2 = SerializationUtils.DeepCopy<Person>(person);
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

    public class Car
    {
        public string Name { get; set; }
        public int Wheel { get; set; }
    }
}
