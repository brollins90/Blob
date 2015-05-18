
namespace Blob.Identity
{
    public class GenericUserClaim : GenericUserClaim<string> { }

    public class GenericUserClaim<TKey>
    {
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }
        public virtual int Id { get; set; }
        public virtual TKey UserId { get; set; }
    }
}
