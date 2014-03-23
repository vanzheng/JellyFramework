using System.Configuration;

namespace Jelly.Database.Configuration
{
    /// <summary>
    /// Represent the config file database DbSection.
    /// </summary>
    public class DatabaseSection : ConfigurationSection
    {
        [ConfigurationProperty("errorLog")]
        public DatabaseErrorLogSettings ErrorLog
        {
            get
            {
                return (DatabaseErrorLogSettings)this["errorLog"];
            }
            set
            {
                this["errorLog"] = value;
            }
        }

        [ConfigurationProperty("infoLog")]
        public DatabaseInfoLogSettings InfoLog
        {
            get
            {
                return (DatabaseInfoLogSettings)this["infoLog"];
            }
            set
            {
                this["infoLog"] = value;
            }
        }
    }
}
