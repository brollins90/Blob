
namespace Blob.Core.Domain
{
    public class Device
    {
        public long Id { get; set; }
        public DeviceType DeviceType { get; set; }
        public Customer Customer { get; set; }
    }
}
