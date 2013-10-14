using System;
using System.IO;

namespace Jelly.Utilities
{
    public static class IOUtility
    {
        /// <summary>
        /// Creates direcotries by specified path or full file name.
        /// </summary>
        /// <param name="path">The specified path or full file name.</param>
        /// <returns></returns>
        public static DirectoryInfo CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            string fullPath = GetFullPath(path);
            if (fullPath.EndsWith("/") || fullPath.EndsWith("\\")) 
            {
                fullPath = fullPath + "\\";    
            }

            string directoryPath = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directoryPath))
            {
                return Directory.CreateDirectory(directoryPath);
            }

            return new DirectoryInfo(fullPath);
        }

        /// <summary>
        /// Gets full path base on current domain base directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFullPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            path = path.Trim();
            if (path.StartsWith("/") || path.StartsWith("\\")) 
            {
                if (path.Length == 1)
                {
                    path = AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {
                    path = path.Substring(1).Replace('/', '\\');
                    path = AppDomain.CurrentDomain.BaseDirectory + path;
                }
            }
            else if (path.StartsWith("~/") || path.StartsWith("~\\"))
            {
                if (path.Length == 2)
                {
                    path = AppDomain.CurrentDomain.BaseDirectory;
                }
                else 
                {
                    path = path.Substring(2).Replace('/', '\\');
                    path = AppDomain.CurrentDomain.BaseDirectory + path;
                }
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
