using Blob.Contracts.Models;
using Blob.Contracts.Status;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Blob.Proxies
{
    public class StatusClient : ClientBase<IStatusService>, IStatusService
    {
        public StatusClient(string endpointName)
            : base(endpointName)
        {
        }

        public StatusClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public async Task SendStatusToServer(StatusData statusData)
        {
            await Channel.SendStatusToServer(statusData).ConfigureAwait(false);
        }

        public async Task SendStatusPerformanceToServer(StatusPerformanceData statusPerformanceData)
        {
            await Channel.SendStatusPerformanceToServer(statusPerformanceData).ConfigureAwait(false);
        }
    }
}
