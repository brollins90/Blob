
namespace Blob.Core.Domain
{
    public abstract class Device : BaseEntity
    {
        public DeviceType DeviceType { get; set; }
        public Customer Customer { get; set; }
    }
}
