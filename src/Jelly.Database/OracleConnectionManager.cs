using System;
using System.Configuration;
using System.Data.OracleClient;

namespace Jelly.Database
{
    public class OracleConnectionManager : DbConnectionManager
    {
        private const string Database_PrividerName = "System.Data.OracleClient";

        public OracleConnectionManager(ConnectionStringSettings connectionStringSettings) : base(connectionStringSettings)
        {
        }

        public OracleConnectionManager(string connectionString)
            : base(connectionString, Database_PrividerName)
        {
        }

        public override string ParameterPrefix
        {
            get { return ":"; }
        }

        public int ExecuteOracleNonQuery(OracleCommand command, out OracleString rowid) 
        {
            PrepareCommand(command);
            try
            {
                int result = command.ExecuteOracleNonQuery(out rowid);
#if DEBUG
                DatabaseLog.WriteInfo(command);
#endif
                return result;
            }
            catch (Exception e)
            {
                DatabaseLog.WriteError(e.Message, command);
                throw;
            }
        }

        public object ExecuteOracleScalar(OracleCommand command) 
        {
            PrepareCommand(command);
            try
            {
                object result = command.ExecuteOracleScalar();
#if DEBUG
                DatabaseLog.WriteInfo(command);
#endif
                return result;
            }
            catch (Exception e)
            {
                DatabaseLog.WriteError(e.Message, command);
                throw;
            }
        }
        
    }
}
