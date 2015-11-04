using Blob.Contracts.ServiceContracts;
using Blob.Services;
using System;
using System.IdentityModel.Services;
using System.Security.Permissions;
using System.ServiceModel;

namespace Blob.WcfHost.Service
{
    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class DeviceConnectionWcfService : IDeviceConnectionService
    {
        IDeviceConnectionService _deviceConnectionService;

        public DeviceConnectionWcfService(IDeviceConnectionService deviceConnectionService)
        {
            _deviceConnectionService = deviceConnectionService;
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "connect")]
        public void Connect(Guid deviceId)
        {
            _deviceConnectionService.Connect(deviceId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "connect")]
        public void Disconnect(Guid deviceId)
        {
            _deviceConnectionService.Disconnect(deviceId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "connect")]
        public void Ping(Guid deviceId)
        {
            _deviceConnectionService.Ping(deviceId);
        }
    }
}