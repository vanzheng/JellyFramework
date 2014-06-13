using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jelly.Caching
{
    /// <summary>
    /// Watchs file whether is changed.
    /// </summary>
    public class FileCacheDependency : ICacheDependency
    {
        private readonly string fileName;
        private FileSystemWatcher fileWatcher;
        private readonly NotifyFilters filters;
        private bool expired;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCacheDependency"/> class.
        /// </summary>
        /// <param name="fileName">The file name with the path.</param>
        /// <param name="filters">The file changed notify type.</param>
        public FileCacheDependency(string fileName, NotifyFilters filters)
        {
            this.fileName = fileName;
            this.filters = filters;
            this.WatchFile();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCacheDependency"/> class.
        /// </summary>
        /// <param name="fileName">The file name with the path.</param>
        /// <remarks>
        /// The watch type is last write time.
        /// </remarks>
        public FileCacheDependency(string fileName)
            : this(fileName, NotifyFilters.LastWrite)
        {
        }

        private void WatchFile()
        {
            var fileName = Path.GetFileName(this.fileName);
            var filePath = Path.GetDirectoryName(this.fileName);
            this.fileWatcher = new FileSystemWatcher(filePath, fileName) { NotifyFilter = this.filters };
            this.fileWatcher.Changed += this.FileChanged;
            this.fileWatcher.EnableRaisingEvents = true;
        }

        private void FileChanged(object sender, FileSystemEventArgs e)
        {
            this.fileWatcher.EnableRaisingEvents = false;
            this.expired = true;
            this.fileWatcher.Dispose();
        }

        public bool Expired 
        {
            get 
            {
                return this.expired;
            }
        }
    }
}
