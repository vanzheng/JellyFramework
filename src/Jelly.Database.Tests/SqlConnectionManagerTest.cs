using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
        public void CreateCommandWithoutParameterTest() 
        {
            using (DbConnectionManager db = ConnectionFactory.Create()) 
            {
                DbCommand command = db.CreateCommand();
                Assert.IsInstanceOfType(command, typeof(SqlCommand));
            }
        }

        [TestMethod]
        public void CreateCommandWithParameterTest()
        {
            using (DbConnectionManager db = ConnectionFactory.Create())
            {
                DbCommand command = db.CreateSqlCommand(Constants.InsertCustomersSqlStatement);
                Assert.IsInstanceOfType(command, typeof(SqlCommand));
                Assert.IsTrue(command.CommandType == CommandType.Text);

                DbCommand command2 = db.CreateStoredProcCommand("p_InsertCustomer");
                Assert.IsInstanceOfType(command2, typeof(SqlCommand));
                Assert.IsTrue(command2.CommandType == CommandType.StoredProcedure);
            }
        }

        [TestMethod]
        public void CreateDataAdapterTest() 
        {
            using (DbConnectionManager db = ConnectionFactory.Create()) 
            {
                DbDataAdapter adapter = db.CreateDataAdapter();
                Assert.IsInstanceOfType(adapter, typeof(SqlDataAdapter));
            }
        }

        [TestMethod]
        public void AddParametersTest() 
        {
            using (DbConnectionManager db = ConnectionFactory.Create())
            {
                DbCommand command = db.CreateSqlCommand(Constants.InsertCustomersSqlStatement);
                db.AddParameter(command, "@CompanyName", DbType.String, ParameterDirection.Input, 255, false, "Company B");
                DbParameter parameter = command.Parameters["@CompanyName"];

                Assert.AreEqual("@CompanyName", parameter.ParameterName);
                Assert.AreEqual(DbType.String, parameter.DbType);
                Assert.AreEqual(ParameterDirection.Input, parameter.Direction);
                Assert.AreEqual(255, parameter.Size);
                Assert.AreEqual(false, parameter.IsNullable);
                Assert.AreEqual("Company B", parameter.Value);

                db.AddInParameter(command, "ContactName", "Lucy");
                DbParameter parameter2 = command.Parameters["@ContactName"];
                Assert.AreEqual("@ContactName", parameter2.ParameterName);
                Assert.AreEqual(DbType.String, parameter2.DbType);
                Assert.AreEqual(ParameterDirection.Input, parameter2.Direction);
                Assert.AreEqual(4, parameter2.Size);
                Assert.AreEqual(false, parameter2.IsNullable);
                Assert.AreEqual("Lucy", parameter2.Value);

                db.AddInParameter(command, "@ContactTitle", DbType.String, "Manager");
                DbParameter parameter3 = command.Parameters["@ContactTitle"];
                Assert.AreEqual("@ContactTitle", parameter3.ParameterName);
                Assert.AreEqual(DbType.String, parameter3.DbType);
                Assert.AreEqual(ParameterDirection.Input, parameter3.Direction);
                Assert.AreEqual(7, parameter3.Size);
                Assert.AreEqual(false, parameter3.IsNullable);
                Assert.AreEqual("Manager", parameter3.Value);

                db.AddInParameter(command, "@Address", DbType.String, null);
                DbParameter parameter4 = command.Parameters["@Address"];
                Assert.AreEqual("@Address", parameter4.ParameterName);
                Assert.AreEqual(DbType.String, parameter4.DbType);
                Assert.AreEqual(ParameterDirection.Input, parameter4.Direction);
                Assert.AreEqual(0, parameter4.Size);
                Assert.AreEqual(false, parameter4.IsNullable);
                Assert.AreEqual(DBNull.Value, parameter4.Value);

                db.AddInParameter(command, "@Age", DbType.Int32, 4, 20);
                DbParameter parameter5 = command.Parameters["@Age"];
                Assert.AreEqual("@Age", parameter5.ParameterName);
                Assert.AreEqual(DbType.Int32, parameter5.DbType);
                Assert.AreEqual(ParameterDirection.Input, parameter5.Direction);
                Assert.AreEqual(4, parameter5.Size);
                Assert.AreEqual(false, parameter5.IsNullable);
                Assert.AreEqual(20, parameter5.Value);
            }
        }

        [TestMethod]
        public void ExecuteNonQueryForInsertTest() 
        {
            using (DbConnectionManager db = ConnectionFactory.Create()) 
            {
                DbCommand command = db.CreateCommand();

                command.CommandText = Constants.InsertCustomersSqlStatement;
                db.AddInParameter(command, "@CompanyName", DbType.String, "Company A");
                db.AddInParameter(command, "@ContactName", DbType.String, "Lily");
                db.AddInParameter(command, "@ContactTitle", DbType.String, "Manager");
                db.AddInParameter(command, "@Address", DbType.String, "Shanghai Xuhui");
                db.AddInParameter(command, "@Phone", DbType.String, "64021202");
                db.AddInParameter(command, "@Fax", DbType.String, "64021203");
                db.AddInParameter(command, "@InsertDate", DbType.DateTime, DateTime.Now);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, DateTime.Now);

                int affectedRows = db.ExecuteNonQuery(command);

                Assert.AreEqual<int>(1, affectedRows);
            }
        }
    }
}
