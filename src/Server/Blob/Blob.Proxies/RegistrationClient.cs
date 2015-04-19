using System;
using Blob.Contracts.Models;
using Blob.Contracts.Registration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Blob.Proxies
{
    public class RegistrationClient : ClientBase<IRegistrationService>, IRegistrationService
    {
        public Action<Exception> ClientErrorHandler = null;

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
            try
            {
                return await Channel.Register(message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        private void HandleError(Exception ex)
        {
            if (ClientErrorHandler != null)
                ClientErrorHandler(ex);
            else
                throw new Exception("Server exception.", ex);
        }
    }
}
