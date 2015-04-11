using Blob.Contracts.Models;
using Blob.Contracts.Registration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Blob.Proxies
{
    public class RegistrationClient : ClientBase<IRegistrationService>, IRegistrationService
    {
        public RegistrationClient(string endpointName)
            : base(endpointName)
        {
        }

        public RegistrationClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public async Task<RegistrationInformation> Register(RegistrationMessage message)
        {
            return await Channel.Register(message).ConfigureAwait(false);
        }
    }
}
