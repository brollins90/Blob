
namespace Blob.Core.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public Customer Customer { get; set; }
    }
}
