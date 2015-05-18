
namespace Blob.Identity
{
    public class GenericUserGroup : GenericUserGroup<string> { }

    public class GenericUserGroup<TKey>
    {
        public virtual TKey GroupId { get; set; }
        public virtual TKey UserId { get; set; }
    }
}
