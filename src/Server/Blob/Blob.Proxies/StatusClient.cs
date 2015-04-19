﻿using System;
using Blob.Contracts.Models;
using Blob.Contracts.Status;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Blob.Proxies
{
    public class StatusClient : ClientBase<IStatusService>, IStatusService
    {
        public Action<Exception> ClientErrorHandler = null;

        public StatusClient(string endpointName)
            : base(endpointName)
        {
        }

        public StatusClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public async Task SendStatusToServer(StatusData statusData)
        {
            try
            {
                await Channel.SendStatusToServer(statusData).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task SendStatusPerformanceToServer(StatusPerformanceData statusPerformanceData)
        {
            try
            {
                await Channel.SendStatusPerformanceToServer(statusPerformanceData).ConfigureAwait(false);
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