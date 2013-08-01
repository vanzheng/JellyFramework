using System;
using System.IO;

namespace Jelly.Utilities
{
    public static class IOUtility
    {
        /// <summary>
        /// Uses safe mode to create diretory folder. 
        /// If the folders are not exist, the method will auto create the folders.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CreateDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            string fullPath = GetFullPath(path);
            string directoryPath = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return fullPath;
        }

        public static string GetFullPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            path = path.Trim();
            if (path.StartsWith("~/"))
            {
                if (path.Length == 2)
                {
                    throw new Exception("The given path '~/' is invalid.");
                }

                path = path.Substring(2).Replace('/', '\\');
                path = AppDomain.CurrentDomain.BaseDirectory + path;
            }

            return path;
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        } 
    }
}
