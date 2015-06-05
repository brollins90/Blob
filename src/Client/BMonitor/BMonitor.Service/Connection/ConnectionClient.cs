using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Blob.Contracts.ServiceContracts;

namespace BMonitor.Service.Connection
{
    public class ConnectionClient : IDeviceConnectionService
    {
        public void Connect(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public void Disconnect(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public void Ping(Guid deviceId)
        {
            throw new NotImplementedException();
        }
    }
}
