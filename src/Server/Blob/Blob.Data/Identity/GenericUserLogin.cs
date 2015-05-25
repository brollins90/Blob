
namespace Blob.Data.Identity
{
    public class GenericUserLogin : GenericUserLogin<string> { }

    public class GenericUserLogin<TKey>
    {
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual TKey UserId { get; set; }
    }
}
