
namespace Blob.Core.Domain
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public Customer Customer { get; set; }
    }
}
