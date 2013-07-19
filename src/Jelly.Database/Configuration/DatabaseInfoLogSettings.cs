using System.Configuration;

namespace Jelly.Database.Configuration
{
    public class DatabaseInfoLogSettings : ConfigurationElement
    {
        [ConfigurationProperty("enabled", DefaultValue = false, IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return bool.Parse((string)this["enabled"]);
            }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("path", IsRequired = true)]
        [RegexStringValidator(@"[^~/\].+")]
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
