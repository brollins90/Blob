
namespace Blob.Core.Identity
{
    public class GenericUserRole : GenericUserRole<string> { }

    public class GenericUserRole<TKey>
    {
        public virtual TKey RoleId { get; set; }
        public virtual TKey UserId { get; set; }
    }
}
