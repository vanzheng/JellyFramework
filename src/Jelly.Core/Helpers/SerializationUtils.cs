using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Jelly.Helpers
{
    public class SerializationUtils
    {
        /// <summary>
        /// Convert xml file to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath">The full xml path which include file name.</param>
        /// <returns>The specified object.</returns>
        public static T XmlToObject<T>(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath)) 
            {
                throw new ArgumentNullException("xmlPath");
            }

            if (!File.Exists(xmlPath)) 
            {
                throw new FileNotFoundException("The xml path is invalid");
            }

            using (StreamReader reader = new StreamReader(xmlPath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                T obj = (T)xs.Deserialize(reader);
                return obj;
            }
        }

        /// <summary>
        /// Convert XmlReader to object.
        /// </summary>
        public static T XmlToObject<T>(XmlReader xmlReader) 
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(xmlReader);

            XmlSerializer xs = new XmlSerializer(typeof(T));
            T obj = (T)xs.Deserialize(xmlReader);
            return obj;
        }

        /// <summary>
        /// Convert object to xml file.
        /// </summary>
        /// <param name="savedXmlPath">The saved xml path which include file name.</param>
        /// <param name="obj">The given specified object.</param>
        public static void ObjectToXml(string savedXmlPath, object obj) 
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(savedXmlPath);

            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (Stream stream = new FileStream(savedXmlPath, FileMode.Create)) 
            {
                serializer.Serialize(stream, obj);
            }
        }

        /// <summary>
        /// Performs deep copy, please note <typeparamref name="T"/> must add <paramref name="SerializableAttribute"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }
    }
}
