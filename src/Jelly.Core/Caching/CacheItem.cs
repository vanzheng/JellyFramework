using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jelly.Caching
{
    /// <summary>
    /// Represents cache item.
    /// </summary>
    public class CacheItem<TKey, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem"/> class.
        /// </summary>
        public CacheItem() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem"/> class.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The cache value</param>
        /// <param name="cacheDependency">The expired cache strategy.</param>
        public CacheItem(TKey key, TValue value, ICacheDependency cacheDependency)
        {
            this.Key = key;
            this.Value = value;
            this.CacheDependency = cacheDependency;
        }

        /// <summary>
        /// Gets cache strategy.
        /// </summary>
        public ICacheDependency CacheDependency { get; set; }

        /// <summary>
        /// Gets the key name.
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets the cache value.
        /// </summary>
        public TValue Value { get; set; }
    }
}
