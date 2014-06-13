
namespace Jelly.Caching
{
    public interface ICacheDependency
    {
        bool Expired { get; }
    }
}
