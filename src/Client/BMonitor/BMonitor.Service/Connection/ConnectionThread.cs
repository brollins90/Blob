using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Blob.Proxies;
using log4net;
using Ninject;

namespace BMonitor.Service.Connection
{
    public class ConnectionThread
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;

        private readonly DeviceConnectionClient _commandClient;
        private readonly Guid _deviceId;

        public ConnectionThread(IKernel kernel, Guid deviceId)//(DeviceConnectionClient client)
        {
            _kernel = kernel;
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _deviceId = deviceId;

            var u = new Ninject.Parameters.ConstructorArgument("username", _deviceId.ToString());
            var p = new Ninject.Parameters.ConstructorArgument("password", _deviceId.ToString());
            _commandClient = _kernel.Get<DeviceConnectionClient>(u, p);
            _commandClient.ClientErrorHandler += HandleException;
        }

        public bool Connected()
        {
            return (_commandClient != null && _commandClient.State == CommunicationState.Opened);
        }

        public void Start()
        {
            if (!Connected())
            {
                _commandClient.Connect(_deviceId);
            }
        }

        public void Stop()
        {
            if (Connected())
            {
                _commandClient.Close();
            }
        }

        private void HandleException(Exception ex)
        {
            _log.Error(string.Format("Error from client proxy."), ex);
        }
    }
}
