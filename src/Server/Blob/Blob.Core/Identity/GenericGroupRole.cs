
namespace Blob.Core.Identity
{
    public class GenericGroupRole : GenericGroupRole<string> { }

    public class GenericGroupRole<TKey>
    {
        public virtual TKey GroupId { get; set; }
        public virtual TKey RoleId { get; set; }
    }
}
