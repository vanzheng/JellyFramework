using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Jelly.Utilities
{
    public static class CopyUtility
    {
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
