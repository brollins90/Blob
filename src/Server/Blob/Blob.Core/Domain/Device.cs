
namespace Blob.Core.Domain
{
    public class Device : BaseEntity
    {
        public DeviceType DeviceType { get; set; }
        public Customer Customer { get; set; }
    }
}
