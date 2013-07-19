using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;

namespace Jelly.Database
{
    /// <summary>
    /// Represents an abstract <see cref="SqlConnection"/> wrapper.
    /// </summary>
    public class SqlConnectionManager : DbConnectionManager
    {
        private const string Database_PrividerName = "System.Data.SqlClient";

        public SqlConnectionManager(ConnectionStringSettings connectionStringSettings) : base(connectionStringSettings)
        {
        }

        public SqlConnectionManager(string connectionString) : base(connectionString, Database_PrividerName)
        {
        }

        /// <summary>
        /// Gets the parameter prefix of the <see cref="DbCommand"/>.
        /// </summary>
        public override string ParameterPrefix
        {
            get { return "@"; }
        }

        public XmlReader ExecuteXmlReader(SqlCommand command) 
        {
            base.PrepareCommand(command);
            try 
            {
                XmlReader reader = command.ExecuteXmlReader();
#if DEBUG
                DatabaseLog.WriteInfo(command);
#endif
                return reader;
            }
            catch(Exception e)
            {
                DatabaseLog.WriteError(e.Message, command);
                throw;
            }
        }
    }
}