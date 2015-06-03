using System;
using System.ServiceModel;
using Blob.Proxies;
using BMonitor.Service.Helpers;
using log4net;
using Ninject;

namespace BMonitor.Service.Connection
{
    public class ConnectionThread : IConnectionThread
    {
        private readonly ILog _log;
        private readonly BlobClientFactory _factory;

        private DeviceConnectionClient _commandClient;
        private readonly Guid _deviceId;

        public ConnectionThread(ILog log, BlobClientFactory factory, Guid deviceId)
        {
            _log = log;
            _factory = factory;
            _deviceId = deviceId;

            _log.Debug("ctor ConnectionThread");
            CreateClient();
        }

        public void CreateClient()
        {
            _commandClient = _factory.CreateDeviceConnectionClient(_deviceId);
            _commandClient.ClientErrorHandler += HandleException;
        }

        public bool IsConnected()
        {
            return (_commandClient != null && _commandClient.State == CommunicationState.Opened);
        }

        public void Start()
        {
            if (!IsConnected())
            {
                _commandClient.Connect(_deviceId);
            }
        }

        public void Stop()
        {
            if (IsConnected())
            {
                _commandClient.Close();
            }
        }

        private void HandleException(Exception ex)
        {
            _log.Error(string.Format("Error from client proxy."), ex);
            try
            {
                _commandClient.Close();
            }
            catch (CommunicationException)
            {
                _commandClient.Abort();
            }
            catch (TimeoutException)
            {
                _commandClient.Abort();
            }
            catch (Exception)
            {
                _commandClient.Abort();
                //throw;
            }
            _commandClient = _factory.CreateDeviceConnectionClient(_deviceId);
            Start();

        }
    }
}
