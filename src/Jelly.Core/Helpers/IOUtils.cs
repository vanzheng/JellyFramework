using System;
using System.IO;

namespace Jelly.Helpers
{
    /// <summary>
    /// IO Utility.
    /// </summary>
    public static class IOUtils
    {
        /// <summary>
        /// Gets full path base on current domain base directory.
        /// </summary>
        /// <param name="fullPath">The path.</param>
        /// <returns>The full path.</returns>
        public static string GetFullPath(string path)
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(path, "path");

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

        /// <summary>
        /// Gets full directory name base on current domain base directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The full directory name.</returns>
        public static string GetFullDirectoryName(string path) 
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNull(path);

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

        /// <summary>
        /// Ensure the path end with slash.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The end with slash path.</returns>
        public static string EnsurePathEndSlash(string path) 
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(path, "path");

            if (!path.EndsWith("/")) 
            {
                return string.Concat(path, "/");
            }

            return path;
        }

        /// <summary>
        /// Converts stream to bytes.
        /// </summary>
        /// <param name="stream">The input stream.</param>
        /// <returns>The bytes.</returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        } 
    }
}
