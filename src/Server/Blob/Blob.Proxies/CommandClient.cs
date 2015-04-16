using Blob.Contracts.Command;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Blob.Proxies
{
    public class CommandClient : ClientBase<ICommandService>, ICommandService
    {
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
            Channel.Connect(deviceId);
        }

        public void Disconnect(Guid deviceId)
        {
            Channel.Disconnect(deviceId);
        }

        public void Ping(Guid deviceId)
        {
            Channel.Ping(deviceId);
        }
    }
}
