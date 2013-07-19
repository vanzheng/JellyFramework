using System.Configuration;
using System.Data.Common;

namespace Jelly.Database
{
    /// <summary>
    /// Represents an abstract <see cref="OleDbConnection"/> wrapper.
    /// </summary>
    public class OleDbConnectionManager : DbConnectionManager
    {
        private const string Database_PrividerName = "System.Data.OleDb";

        public OleDbConnectionManager(ConnectionStringSettings connectionStringSettings) : base(connectionStringSettings)
        {
        }

        public OleDbConnectionManager(string connstr) : base(connstr, Database_PrividerName) 
        {
        }

        /// <summary>
        /// Gets the parameter prefiex of the <see cref="DbCommand"/>.
        /// </summary>
        public override string ParameterPrefix 
        {
            get { return "@"; }
        }        
    }
}