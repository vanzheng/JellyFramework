using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Jelly.Database
{
    /// <summary>
    /// Represents an abstract <see cref="DbConnection"/> wrapper.
    /// </summary>
    public abstract class DbConnectionManager : IDisposable
    {
        private DbProviderFactory _dbProviderFactory = null;
        private DbConnection _dbConnection = null;
        private DbTransaction _dbTransaction = null;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionManager"/> class.
        /// </summary>
        /// <param name="connectionStringSettings">The <see cref="ConnectionStringSettings"/> object.</param>
        protected DbConnectionManager(ConnectionStringSettings connectionStringSettings)
        {
            this.InitConnection(connectionStringSettings.ConnectionString, connectionStringSettings.ProviderName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionManager"/> class.
        /// </summary>
        protected DbConnectionManager(string connectionString, string providerName) 
        {
            this.InitConnection(connectionString, providerName);
        }

        #endregion

        #region Properties

        public string ConnectionString 
        {
            get 
            {
                return _dbConnection.ConnectionString;
            }
        }

        public string Database 
        {
            get 
            {
                return _dbConnection.Database;
            }
        }

        public string DataSource 
        {
            get 
            {
                return _dbConnection.DataSource;
            }
        }

        public ConnectionState ConnectionState 
        {
            get 
            {
                return _dbConnection.State;
            }
        }

        public int ConnectionTimeout 
        {
            get 
            {
                return _dbConnection.ConnectionTimeout;
            }
        }

        public abstract string ParameterPrefix
        {
            get;
        }

        #endregion

        #region Non-Public Methods

        private void InitConnection(string connectionString, string providerName)
        {
            this._dbProviderFactory = DbProviderFactories.GetFactory(providerName);
            this._dbConnection = _dbProviderFactory.CreateConnection();
            this._dbConnection.ConnectionString = connectionString;
        }

        protected virtual string EnsureParameterPrefix(string parameterName) 
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName");
            }

            if (parameterName.StartsWith(ParameterPrefix))
            {
                return parameterName;
            }
            else
            {
                return string.Concat(ParameterPrefix, parameterName);
            }
        }

        protected virtual DbParameter CreateParameter()
        {
            DbParameter parameter = _dbProviderFactory.CreateParameter();
            return parameter;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the <see cref="DbCommand"/> object.
        /// </summary>
        /// <returns>The <see cref="DbCommand"/> object.</returns>
        public virtual DbCommand CreateCommand() 
        {
            DbCommand command = _dbProviderFactory.CreateCommand();
            return command;
        }

        /// <summary>
        /// Creates the <see cref="DbCommand"/> object with sql statement.
        /// </summary>
        /// <returns>The <see cref="DbCommand"/> object.</returns>
        public DbCommand CreateSqlCommand(string sql)
        {
            DbCommand command = CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            return command;
        }

        /// <summary>
        /// Creates the <see cref="DbCommand"/> object with stored procedure name.
        /// </summary>
        /// <returns>The <see cref="DbCommand"/> object.</returns>
        public DbCommand CreateStoredProcCommand(string storedProcName)
        {
            DbCommand command = CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcName;
            return command;
        }

        /// <summary>
        /// Creates the <see cref="DataAdapter"/> object.
        /// </summary>
        /// <returns>The <see cref="DbDataAdapter"/> object.</returns>
        public virtual DbDataAdapter CreateDataAdapter() 
        {
            DbDataAdapter adapter = _dbProviderFactory.CreateDataAdapter();
            return adapter;
        }

        /// <summary>
        /// Creates the <see cref="DbTransaction"/> object.
        /// </summary>
        /// <returns>The <see cref="DbTransaction"/> object.</returns>
        public void BeginTransaction()
        {
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        /// <summary>
        /// Creates the <see cref="DbTransaction"/> object with given a <paramref name="level"/> object.
        /// </summary>
        /// <param name="level">The <see cref="IsolationLevel"/> object.</param>
        /// <returns></returns>
        public void BeginTransaction(IsolationLevel level)
        {
            _dbTransaction = _dbConnection.BeginTransaction(level);
        }

        public void AddInParameter(DbCommand command, string parameterName, object value)
        {
            DbParameter parameter = CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

        public void AddInParameter(DbCommand command, string parameterName, DbType dbType, object value)
        {
            AddParameter(command, parameterName, dbType, ParameterDirection.Input, 0, false, value);
        }

        public void AddInParameter(DbCommand command, string parameterName, DbType dbType, int size, object value)
        {
            AddParameter(command, parameterName, dbType, ParameterDirection.Input, size, false, value);
        }

        public void AddOutParameter(DbCommand command, string parameterName, DbType dbType)
        {
            AddParameter(command, parameterName, dbType, ParameterDirection.Output, 0, false, null);
        }

        public void AddOutParameter(DbCommand command, string parameterName, DbType dbType, int size)
        {
            AddParameter(command, parameterName, dbType, ParameterDirection.Output, size, false, null);
        }

        public void AddInOutParameter(DbCommand command, string parameterName, DbType dbType, object value)
        {
            AddParameter(command, parameterName, dbType, ParameterDirection.Output, 0, false, value);
        }

        public void AddInOutParameter(DbCommand command, string parameterName, DbType dbType, int size, object value)
        {
            AddParameter(command, parameterName, dbType, ParameterDirection.Output, size, false, value);
        }

        public void AddReturnParameter(DbCommand command, string parameterName, DbType dbType)
        {
            AddParameter(command, parameterName, dbType, ParameterDirection.ReturnValue, 0, false, null);
        }

        public void AddReturnParameter(DbCommand command, string parameterName, DbType dbType, int size)
        {
            AddParameter(command, parameterName, dbType, ParameterDirection.ReturnValue, size, false, null);
        }

        public void AddParameter(DbCommand command, string parameterName, DbType dbType, ParameterDirection direction, int size, bool nullable, object value)
        {
            DbParameter dbParameter = CreateParameter();
            dbParameter.ParameterName = EnsureParameterPrefix(parameterName);
            dbParameter.DbType = dbType;
            dbParameter.Direction = direction;
            dbParameter.Size = size;
            dbParameter.IsNullable = nullable;
            dbParameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(dbParameter);
        }

        /// <summary>
        /// Gest the <see cref="DbParameter"/> value with given <paramref name="command"/>.
        /// </summary>
        /// <param name="cmd">The given <see cref="DbCommand"/> object.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>The value of parameter.</returns>
        public object GetParamterValue(DbCommand command, string paramName)
        {
            return ((DbParameter)command.Parameters[EnsureParameterPrefix(paramName)]).Value;
        }

        protected void PrepareCommand(DbCommand command)
        {
            if (command == null) 
            {
                throw new ArgumentNullException("command");
            }

            if (this._dbConnection.State != ConnectionState.Open) 
            {
                this._dbConnection.Open();
            }
            command.Connection = this._dbConnection;
            if (this._dbTransaction != null) 
            {
                command.Transaction = this._dbTransaction;
            }
        }

        /// <summary>
        /// Executes the <see cref="DbCommand"/> and returns the number of rows affected.
        /// </summary>
        /// <param name="command">The command that contains the query to execute.</param>
        /// <returns>The number of rows affected.</returns>
        public virtual int ExecuteNonQuery(DbCommand command)
        {
            PrepareCommand(command);
            try
            {
                int result = command.ExecuteNonQuery();
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

        /// <summary>
        /// Executes the SQL statement and returns the number of rows affected.
        /// </summary>
        /// <param name="sql">The SQL statment.</param>
        /// <returns>The number of rows affected.</returns>
        public virtual int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(CreateSqlCommand(sql));
        }

        /// <summary>
        /// Executes the <see cref="DbCommand"/> and returns the first column of the first row in the result set returned by the query.
        /// </summary>
        /// <param name="command">The <see cref="DbCommand"/> that contains the query to execute.</param>
        /// <returns>The first column of the first row in the result set.</returns>
        public virtual object ExecuteScalar(DbCommand command)
        {
            PrepareCommand(command);
            try
            {
                object obj = command.ExecuteScalar();
#if DEBUG
                DatabaseLog.WriteInfo(command);
#endif
                return obj;
            }
            catch (Exception e)
            {
                DatabaseLog.WriteError(e.Message, command);
                throw;
            }
        }

        /// <summary>
        /// Executes the SQL statement and returns the first column of the first row in the result set returned by the query.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>The first column of the first row in the result set.</returns>
        public virtual object ExecuteScalar(string sql)
        {
            return ExecuteScalar(CreateSqlCommand(sql));
        }

        /// <summary>
        /// Executes the <see cref="DbCommand"/> and return <see cref="DataReader"/> object.
        /// </summary>
        /// <param name="command">The <see cref="DbCommand"/> that contains the query to execute.</param>
        /// <returns>The <see cref="DataReader"/> object.</returns>
        public virtual DbDataReader ExecuteReader(DbCommand command)
        {
            PrepareCommand(command);
            try
            {
                DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
#if DEBUG
                DatabaseLog.WriteInfo(command);
#endif
                return reader;
            }
            catch (Exception e)
            {
                _dbConnection.Dispose();
                DatabaseLog.WriteError(e.Message, command);
                throw;
            }
        }

        /// <summary>
        /// Executes the SQL statement and returns the <see cref="DataReader"/>.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>The <see cref="DataReader"/> object.</returns>
        public virtual DbDataReader ExecuteReader(string sql)
        {
            return ExecuteReader(CreateSqlCommand(sql));
        }

        /// <summary>
        /// Executes the <paramref name="command"/> and returns the results in a new <see cref="DataSet"/>.
        /// </summary>
        /// <param name="command">The <see cref="DbCommand"/> that contains the query to execute.</param>
        /// <returns>The <see cref="DataSet"/> object.</returns>
        public virtual DataSet ExecuteDataSet(DbCommand command)
        {
            PrepareCommand(command);
            {
                try
                {
                    IDbDataAdapter adapter = CreateDataAdapter();
                    DataSet ds = new DataSet();
                    adapter.SelectCommand = command;
                    adapter.Fill(ds);
#if DEBUG
                    DatabaseLog.WriteInfo(command);
#endif
                    return ds;
                }
                catch (Exception e)
                {
                    DatabaseLog.WriteError(e.Message, command);
                    throw;
                }
            }
        }

        /// <summary>
        /// Executes the SQL statement and returns <see cref="DataSet"/> object.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>The <see cref="DataSet"/> object.</returns>
        public virtual DataSet ExecuteDataSet(string sql)
        {
            return ExecuteDataSet(CreateSqlCommand(sql));
        }

        public virtual DataTable GetSchema() 
        {
            return _dbConnection.GetSchema();
        }

        public virtual DataTable GetSchema(string collectionName) 
        {
            return _dbConnection.GetSchema(collectionName);
        }

        public virtual DataTable GetSchema(string collectionName, string[] restrictionValues) 
        {
            return _dbConnection.GetSchema(collectionName, restrictionValues);
        }

        /// <summary>
        /// Executes the <paramref name="command"/> and returns the <see cref="IList<T>"/> object.
        /// </summary>
        /// <param name="command">The <see cref="DbCommand"/> that contains the query to execute.</param>
        /// <param name="dataReader">The <see cref="DataReaderToModel"/> object.</param>
        /// <returns>The <see cref="IList<T>"/> object.</returns>
        public virtual IList<T> GetListModel<T>(DbCommand command, DbDataReaderToModel<T> dataReader) where T : class
        {
            IList<T> list = new List<T>();
            using (DbDataReader dr = ExecuteReader(command))
            {
                while (dr.Read())
                {
                    list.Add(dataReader(dr));
                }
            }
            return list;
        }

        /// <summary>
        /// Executes the SQL statement and returns the <see cref="IList<T>"/> object.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <param name="dataReader">The <see cref="DataReaderToModel"/> object.</param>
        /// <returns>The <see cref="IList<T>"/> object.</returns>
        public virtual IList<T> GetListModel<T>(string sql, DbDataReaderToModel<T> dataReader) where T : class
        {
            return GetListModel<T>(CreateSqlCommand(sql), dataReader);
        }

        public void CommitTransaction() 
        {
            _dbTransaction.Commit();
        }

        public void RollbackTransaction() 
        {
            _dbTransaction.Rollback();
        }

        public void Dispose()
        {
            if (_dbTransaction != null) 
            {
                _dbTransaction.Dispose();
            }

            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
            }
        }

        #endregion
    }
}
