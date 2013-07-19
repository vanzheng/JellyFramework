
namespace Jelly.Caching
{
    public interface ICacheManager<TKey, TValue>
    {
        TValue Get(TKey key);
        void Insert(TKey key, TValue value);
        // To do: add dependency for cache.
        //void Insert(TKey key, TValue value, ICacheDependency dependency);
        TValue this[TKey key] { get; set; }
        bool ContainsKey(TKey key);
        bool ContainsValue(TValue value);
        bool Remove(TKey key);
        void Clear();
        int Count { get; }
    }
}
