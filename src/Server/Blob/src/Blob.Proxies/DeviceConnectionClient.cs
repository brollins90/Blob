namespace Blob.Proxies
{
    using System;
    using System.ServiceModel;
    using Contracts.ServiceContracts;

    public class DeviceConnectionClient : BaseDuplexClient<IDeviceConnectionService>, IDeviceConnectionService
    {
        public DeviceConnectionClient(string endpointName, string username, string password, InstanceContext callbackInstance)
            : base(endpointName, username, password, callbackInstance)
        { }

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