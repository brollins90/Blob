using System;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using log4net;

namespace BMonitor.Service
{
    public class Registrator
    {
        private readonly ILog _log;

        public Registrator(ILog log)
        {
            _log = log;
        }

        public bool IsRegistered()
        {
            return _registered;
        }

        private bool _registered;

        public Guid RegisterDevice(IDeviceStatusService statusClient)
        {
            Guid deviceGuid = Guid.NewGuid();
            _log.Info(string.Format("Registering this agent with the BlobService with id:{0}.", deviceGuid));

            RegisterDeviceDto regMessage = new RegisterDeviceDto
            {
                DeviceId = deviceGuid.ToString(),
                DeviceKey1 = "FutureUse",
                DeviceKey1Format = 0,
                DeviceKey2 = "FutureUse",
                DeviceKey2Format = 0,
                DeviceName = System.Environment.MachineName,
                DeviceType = "WindowsDesktop",
                TimeSent = DateTime.Now
            };
            _log.Debug(string.Format("RegistrationMessage request: {0}", regMessage));
            
            RegisterDeviceResponseDto regInfo = Task.Run(() => statusClient.RegisterDeviceAsync(regMessage)).Result;

            //WcfServiceInvoker invoker = new WcfServiceInvoker();
            //RegisterDeviceResponseDto regInfo = invoker.InvokeService<IDeviceStatusService, RegisterDeviceResponseDto>(
            //    proxy => proxy.RegisterDeviceAsync(regMessage)
            //);
            //_log.Debug(string.Format("RegistrationInformation response: {0}", regInfo));
            if (regInfo.Succeeded)
            {
                _registered = true;
                return regInfo.DeviceId;
            }
            _log.Error("Registration failed: " + regInfo.Errors);
            
            _registered = false;
            return Guid.Empty;
        }

        public void UnRegisterDevice()
        {
            throw new NotImplementedException();
        }
    }
}
