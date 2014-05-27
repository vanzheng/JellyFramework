using System;
using System.IO;

namespace Jelly.Helpers
{
    public static class IOUtils
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

        public static string GetFullDirectoryName(string path) 
        {
            ExceptionManager.ThrowIfNull(path, "The page is invalid");

            string fullPath = GetFullPath(path);
            string ext = Path.GetExtension(fullPath);

            if (string.IsNullOrEmpty(ext))
            {
                return fullPath;
            }
            else 
            {
                return Path.GetDirectoryName(fullPath);
            }
        }

        public static string EnsurePathEndSlash(string path) 
        {
            ExceptionManager.ThrowIfNullOrEmpty(path, "The path is invalid");

            if (!path.EndsWith("/")) 
            {
                return string.Concat(path, "/");
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
