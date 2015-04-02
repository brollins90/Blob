using Blob.Contracts.Models;
using Blob.Contracts.Status;
using Blob.Managers.Status;
using System;

namespace Blob.Services.Status
{
    public class StatusService : IStatusService
    {
        private readonly IStatusManager _statusManager;

        public StatusService(IStatusManager statusManager)
        {
            _statusManager = statusManager;
        }

        public void SendStatusToServer(StatusData statusData)
        {
            Console.WriteLine("Got Status: {0}", statusData);
            _statusManager.StoreStatusData(statusData);
        }
    }
}
