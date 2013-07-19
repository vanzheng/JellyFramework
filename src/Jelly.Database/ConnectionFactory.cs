using System;
using System.Collections.Specialized;
using System.Configuration;
using Jelly.Caching;

namespace Jelly.Database
{
    /// <summary>
    /// ConnectionFactory create DbConnectionManager object.
    /// </summary>
    public class ConnectionFactory
    {
        private const string DefaultConnectionStringName = "DefaultConnectionStringNodeName";
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static CacheManager<string, DbConnectionManager> cachedConnection = new CacheManager<string, DbConnectionManager>();
        private static CacheManager<string, ConnectionStringSettings> cachedSettings = new CacheManager<string, ConnectionStringSettings>();

        /// <summary>
        /// Creates the default <see cref="DbConnectionManager"/> object. 
        /// </summary>
        /// <returns></returns>
        public static DbConnectionManager Create()
        {
            return Create(GetDefaultConnectionStringNodeName());
        }

        /// <summary>
        /// Creates the <see cref="DbProvider"/> object.
        /// </summary>
        /// <param name="connstrNodeName">The connectionString node name.</param>
        /// <returns></returns>
        public static DbConnectionManager Create(string connstrNodeName)
        {
            ConnectionStringSettings settings;
            if (cachedSettings[connstrNodeName] != null)
            {
                settings = cachedSettings[connstrNodeName];
            }
            else 
            {
                settings = ConfigurationManager.ConnectionStrings[connstrNodeName];
                cachedSettings.Insert(connstrNodeName, settings);
            }

            // Search DbConnectionManager object in cache.
            if (cachedConnection[settings.ConnectionString] != null) 
            {
                return cachedConnection[settings.ConnectionString];
            }

            DbConnectionManager connection = (DbConnectionManager)Activator.CreateInstance(Type.GetType(GetConnectionTypeName(settings)), new object[] { settings });
            cachedConnection[settings.ConnectionString] = connection;
            return connection;
        }

        private static string GetDefaultConnectionStringNodeName()
        {
            if (appSettings != null && appSettings[DefaultConnectionStringName] != null)
            {
                return appSettings[DefaultConnectionStringName];
            }

            return null;
        }

        private static string GetConnectionTypeName(ConnectionStringSettings settings) 
        {
            switch (settings.ProviderName) 
            {
                case Constants.OleDbProviderName:
                    return Constants.OleDbConnectionManagerTypeName;
                case Constants.SqlClientProviderName:
                    return Constants.SqlConnectionManagerTypeName;
                default:
                    return Constants.SqlConnectionManagerTypeName;
            }
        }
    }
}
