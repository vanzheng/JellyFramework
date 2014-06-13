using System;
using System.Collections.Generic;
using System.Threading;
using Jelly.Core;

namespace Jelly.Caching
{
    public class CacheManager<TKey, TValue> : ScheduleTimer, ICacheManager<TKey, TValue>
    {
        private ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
        private CacheItemCollection<TKey, TValue>_collection;

        public CacheManager() 
        {
            this._collection = new CacheItemCollection<TKey, TValue>();    
        }

        public CacheManager(int capacity) 
        {
            this._collection = new CacheItemCollection<TKey, TValue>(capacity); 
        }

        protected override void HandleOnTime()
        {
            foreach (TKey key in this._collection.Keys) 
            {
                var cacheItem = this._collection.Get(key);

                if (cacheItem != null && cacheItem.CacheDependency.Expired) 
                {
                    this._collection.Remove(key);
                }
            }
        }

        public TValue Get(TKey key) 
        {
            if (key == null) 
            {
                throw new ArgumentNullException("key");
            }

            var cacheItem = this._collection.Get(key);

            if (cacheItem != null) 
            {
                return cacheItem.Value;
            }
            
            return default(TValue);
        }

        public void Insert(TKey key, TValue value) 
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            Insert(key, value, new NullCacheDependency());
        }

        public void Insert(TKey key, TValue value, ICacheDependency cacheDependency)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            var cacheItem = new CacheItem<TKey, TValue>(key, value, cacheDependency);
            this._collection.Insert(key, cacheItem);
            
            base.StartTimer();
        }

        public TValue this[TKey key]
        {
            get 
            {
                return this.Get(key);
            }
            set 
            {
                this.Insert(key, value);
            }
        }

        public bool ContainsKey(TKey key) 
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            return this._collection.ContainsKey(key);
        }

        public bool Remove(TKey key) 
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            return this._collection.Remove(key);
        }

        public int Count 
        {
            get 
            {
                return this._collection.Count;
            }
        }

        public void Clear() 
        {
            this._collection.Clear();
        }
    }
}
