using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using Blob.Contracts.Command;

namespace Blob.Proxies
{
    public class DeviceConnectionClient : ClientBase<IDeviceConnectionService>, IDeviceConnectionService
    {
        public Action<Exception> ClientErrorHandler = null;

        public DeviceConnectionClient(InstanceContext callbackInstance, string endpointName, string username, string password) : base(callbackInstance, endpointName)
        {
            ClientCredentials.UserName.UserName = username;
            ClientCredentials.UserName.Password = password;
            ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
        }
        //public DeviceConnectionClient(InstanceContext callbackInstance, Binding binding, EndpointAddress address) : base(callbackInstance, binding, address) { }
        
        private void HandleError(Exception ex)
        {
            if (ClientErrorHandler != null)
                ClientErrorHandler(ex);
            else
                throw new Exception("Server exception.", ex);
        }

        public void Connect(Guid deviceId)
        {
            try
            {
                Channel.Connect(deviceId);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public void Disconnect(Guid deviceId)
        {
            try
            {
                Channel.Disconnect(deviceId);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public void Ping(Guid deviceId)
        {
            try
            {
                Channel.Ping(deviceId);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
    }
}
