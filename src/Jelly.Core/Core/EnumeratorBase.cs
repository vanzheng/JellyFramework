using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jelly.Core
{
    public abstract class EnumeratorBase<T> : IEnumerator<T>
    {
        private T[] _items;
        private int _currentIndex;

        public EnumeratorBase(T[] items)
        {
            this._items = items;
            this._currentIndex = -1;
        }

        public bool MoveNext()
        {
            this._currentIndex++;
            return this._currentIndex < this._items.Length;
        }

        public void Reset() 
        { 
            this._currentIndex = -1; 
        }

        void IDisposable.Dispose() 
        { 
        
        }

        public T Current
        {
            get 
            {
                return this._items[this._currentIndex];
            }
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }
    }
}
