using System;
using System.Data;
using System.Data.Common;
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

        [TestMethod]
        public void ExecuteNonQueryForInsertTest() 
        {
            using (DbConnectionManager db = ConnectionFactory.Create()) 
            {
                DbCommand command = db.CreateCommand();

                command.CommandText = "INSERT INTO Customers(CompanyName, ContactName, ContactTitle, Address, Phone, Fax, InsertDate, UpdateDate)"
                    + " VALUES(@CompanyName, @ContactName, @ContactTitle, @Address, @Phone, @Fax, @InsertDate, @UpdateDate)";
                db.AddInParameter(command, "@CompanyName", DbType.String, "Company A");
                db.AddInParameter(command, "@ContactName", DbType.String, "Lily");
                db.AddInParameter(command, "@ContactTitle", DbType.String, "Manager");
                db.AddInParameter(command, "@Address", DbType.String, "Shanghai Xuhui");
                db.AddInParameter(command, "@Phone", DbType.String, "64021202");
                db.AddInParameter(command, "@Fax", DbType.String, "64021203");
                db.AddInParameter(command, "@InsertDate", DbType.DateTime, DateTime.Now);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, DateTime.Now);

                int affectRecord = db.ExecuteNonQuery(command);

                Assert.AreEqual<int>(1, affectRecord);
            }
        }
    }
}
