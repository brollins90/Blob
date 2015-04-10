
namespace Blob.Core.Domain
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }

        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
