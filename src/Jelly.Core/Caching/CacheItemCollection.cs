using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Jelly.Caching
{
    public class CacheItemCollection<TKey, TValue>
    {
        private ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
        private Dictionary<TKey, CacheItem<TKey, TValue>> _dictionary;

        public CacheItemCollection() 
        {
            this._dictionary = new Dictionary<TKey, CacheItem<TKey, TValue>>(); 
        }

        public CacheItemCollection(int capacity) 
        {
            this._dictionary = new Dictionary<TKey, CacheItem<TKey, TValue>>(capacity);
        }

        public Dictionary<TKey, CacheItem<TKey, TValue>>.KeyCollection Keys 
        {
            get 
            {
                try
                {
                    lockSlim.EnterReadLock();
                    return this._dictionary.Keys;
                }
                finally 
                {
                    lockSlim.ExitReadLock();
                }
            }
        }

        public Dictionary<TKey, CacheItem<TKey, TValue>>.ValueCollection Values 
        {
            get 
            {
                try
                {
                    lockSlim.EnterReadLock();
                    return this._dictionary.Values;
                }
                finally 
                {
                    lockSlim.ExitReadLock();
                }
            }
        }

        public CacheItem<TKey, TValue> Get(TKey key) 
        {
            if (key == null) 
            {
                throw new ArgumentNullException("key");
            }

            CacheItem<TKey, TValue> item = null;

            try
            {
                lockSlim.EnterReadLock();
                this._dictionary.TryGetValue(key, out item);
            }
            finally 
            {
                lockSlim.ExitReadLock();
            }

            return item;
        }

        public void Insert(TKey key, CacheItem<TKey, TValue> item) 
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            try
            {
                lockSlim.EnterWriteLock();
                this._dictionary[key] = item;
            }
            finally 
            {
                lockSlim.ExitWriteLock();
            }
        }

        public CacheItem<TKey, TValue> this[TKey key]
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
                result = this._dictionary.ContainsKey(key);
            }
            finally 
            {
                lockSlim.ExitReadLock();
            }

            return result;
        }

        public bool ContainsValue(CacheItem<TKey, TValue> item)
        {
            bool result = false;
            
            try
            {
                lockSlim.EnterReadLock();
                result = this._dictionary.ContainsValue(item);
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
                result = this._dictionary.Remove(key);
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
                    count = this._dictionary.Count;
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
                this._dictionary.Clear();
            }
            finally 
            {
                lockSlim.ExitWriteLock();
            }
        }
    }
}
