
namespace Jelly.Caching
{
    public interface ICacheManager<TKey, TValue>
    {
        TValue Get(TKey key);
        void Insert(TKey key, TValue value);
        void Insert(TKey key, TValue value, ICacheDependency dependency);
        TValue this[TKey key] { get; set; }
        bool ContainsKey(TKey key);
        bool Remove(TKey key);
        void Clear();
        int Count { get; }
    }
}
