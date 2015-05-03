using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Registration;

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

        public async Task Register(RegisterDeviceDto message)
        {
            try
            {
                await Channel.Register(message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
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
