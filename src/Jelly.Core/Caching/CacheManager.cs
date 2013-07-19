using System;
using System.Collections.Generic;
using System.Threading;

namespace Jelly.Caching
{
    public class CacheManager<TKey, TValue> : ICacheManager<TKey, TValue>
    {
        private ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
        private Dictionary<TKey, TValue> _cache;

        public CacheManager() 
        {
            this._cache = new Dictionary<TKey, TValue>();    
        }

        public CacheManager(int capacity) 
        {
            this._cache = new Dictionary<TKey, TValue>(capacity);
        }

        public TValue Get(TKey key) 
        {
            if (key == null) 
            {
                throw new ArgumentNullException("key");
            }

            TValue value = default(TValue);

            try
            {
                lockSlim.EnterReadLock();
                this._cache.TryGetValue(key, out value);
            }
            finally 
            {
                lockSlim.ExitReadLock();
            }

            return value;
        }

        public void Insert(TKey key, TValue value) 
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            try
            {
                lockSlim.EnterWriteLock();
                this._cache[key] = value;
            }
            finally 
            {
                lockSlim.ExitWriteLock();
            }
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

            bool result = false;
            
            try
            {
                lockSlim.EnterReadLock();
                result = this._cache.ContainsKey(key);
            }
            finally 
            {
                lockSlim.ExitReadLock();
            }

            return result;
        }

        public bool ContainsValue(TValue value)
        {
            bool result = false;
            
            try
            {
                lockSlim.EnterReadLock();
                result = this._cache.ContainsValue(value);
            }
            finally 
            {
                lockSlim.ExitReadLock();
            }

            return result;
        }

        public bool Remove(TKey key) 
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            bool result = false;
            
            try
            {
                lockSlim.EnterWriteLock();
                result = this._cache.Remove(key);
            }
            finally 
            {
                lockSlim.ExitWriteLock();
            }

            return result;
        }

        public int Count 
        {
            get 
            {
                int count;
                
                try
                {
                    lockSlim.EnterReadLock();
                    count = this._cache.Count;
                }
                finally 
                {
                    lockSlim.ExitReadLock();
                }

                return count;
            }
        }

        public void Clear() 
        {
            try
            {
                lockSlim.EnterWriteLock();
                this._cache.Clear();
            }
            finally 
            {
                lockSlim.ExitWriteLock();
            }
        }
    }
}
