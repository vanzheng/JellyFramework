using System.Configuration;

namespace Jelly.Database.Configuration
{
    public class DatabaseErrorLogSettings : ConfigurationElement
    {
        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set 
            { 
                this["enabled"] = value; 
            }
        }

        [ConfigurationProperty("path", IsRequired = true)]
        public string LogPath
        {
            get
            {
                return (string)this["path"];
            }
            set
            {
                this["path"] = value;
            }
        }

        [ConfigurationProperty("dateTimePattern")]
        public string DateTimePattern
        {
            get
            {
                return (string)this["dateTimePattern"];
            }
            set
            {
                this["dateTimePattern"] = value;
            }
        }
    }
}
