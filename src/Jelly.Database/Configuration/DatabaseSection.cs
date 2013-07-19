using System.Configuration;

namespace Jelly.Database.Configuration
{
    /// <summary>
    /// Represent the config file database section.
    /// </summary>
    public class DatabaseSection : ConfigurationSection
    {
        public DatabaseSection() 
        {
        
        }

        [ConfigurationProperty("log")]
        public DatabaseLogSettings Log 
        {
            get 
            {
                return (DatabaseLogSettings)base["log"];
            }
        }
    }
}
