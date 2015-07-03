namespace Blob.Core.Identity.Models
{
    public interface IGroup<out TKey>
    {
        TKey Id { get; }
        string Name { get; set; }
    }
}