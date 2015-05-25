
namespace Blob.Data.Identity
{
    public interface IGroup<out TKey>
    {
        TKey Id { get; }
        string Name { get; set; }
    }
}
