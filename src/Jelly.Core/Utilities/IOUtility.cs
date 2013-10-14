using System;
using System.IO;

namespace Jelly.Utilities
{
    public static class IOUtility
    {
        /// <summary>
        /// Gets full path base on current domain base directory.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string GetFullPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            string fullPath = path.Trim();
            if (fullPath.StartsWith("/") || fullPath.StartsWith("\\")) 
            {
                if (fullPath.Length == 1)
                {
                    fullPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, Path.DirectorySeparatorChar);
                }
                else
                {
                    fullPath = fullPath.Substring(1).Replace('/', '\\');
                    fullPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, Path.DirectorySeparatorChar, fullPath);
                }
            }
            else if (fullPath.StartsWith("~/") || fullPath.StartsWith("~\\"))
            {
                if (fullPath.Length == 2)
                {
                    fullPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, Path.DirectorySeparatorChar);
                }
                else 
                {
                    fullPath = fullPath.Substring(2).Replace('/', '\\');
                    fullPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, Path.DirectorySeparatorChar, fullPath);
                }
            }

            return fullPath;
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
