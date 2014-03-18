using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Jelly.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Database.Tests
{
    [TestClass]
    public class SqlConnectionManagerTest
    {
        [TestMethod]
        public void ConnectioFactoryCreateTest()
        {
            using (DbConnectionManager db = ConnectionFactory.Create()) 
            {
                Assert.IsNotNull(db);
            }
        }
    }
}
