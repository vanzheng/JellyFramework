using System.Configuration;

namespace Jelly.Database.Configuration
{
    /// <summary>
    /// Represents the database log setting element.
    /// </summary>
    public class DatabaseLogSettings : ConfigurationElement
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
