using System;
using Blob.Contracts.Status;

namespace Blob.Services.Status
{
    public class StatusService : IStatusService
    {
        public void SendStatusToServer(string message)
        {
            Console.WriteLine("Got Status: {0}", message);
        }
    }
}
