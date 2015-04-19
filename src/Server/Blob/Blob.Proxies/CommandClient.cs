using Blob.Contracts.Command;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Blob.Proxies
{
    public class CommandClient : ClientBase<ICommandService>, ICommandService
    {
        public Action<Exception> ClientErrorHandler = null;

        public CommandClient(InstanceContext callbackInstance, string endpointName)
            : base(callbackInstance, endpointName)
        {
        }

        public CommandClient(InstanceContext callbackInstance, Binding binding, EndpointAddress address)
            : base(callbackInstance, binding, address)
        {
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

        private void HandleError(Exception ex)
        {
            if (ClientErrorHandler != null)
                ClientErrorHandler(ex);
            else
                throw new Exception("Server exception.", ex);
        }
    }
}
