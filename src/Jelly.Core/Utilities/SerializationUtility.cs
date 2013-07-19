using System;
using System.IO;
using System.Xml.Serialization;

namespace Jelly.Utilities
{
    public class SerializationUtility
    {
        public static object XmlToObject(string path, Type type)
        {
            if (string.IsNullOrWhiteSpace(path)) 
            {
                throw new ArgumentNullException("path");
            }

            if (!File.Exists(path)) 
            {
                return null;
            }

            using (StreamReader reader = new StreamReader(path))
            {
                XmlSerializer xs = new XmlSerializer(type);
                object obj = xs.Deserialize(reader);
                return obj;
            }
        }
    }
}
