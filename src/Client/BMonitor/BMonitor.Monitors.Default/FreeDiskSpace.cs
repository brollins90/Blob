using Blob.Contracts.Models;
using BMonitor.Common.Extensions;
using BMonitor.Common.Interfaces;
using System;
using System.IO;

namespace BMonitor.Monitors.Default
{
    public class FreeDiskSpace : IMonitor
    {
        public FreeDiskSpace()
        {
            // todo: create a config
        }

        public StatusData Execute()
        {
            StatusData status = new StatusData()
            {
                MonitorDescription = "Free Disk Space: ",
                MonitorName = "FreeDiskSpace",
                TimeGenerated = DateTime.Now
            };

            DriveInfo driveInfo = new DriveInfo(@"C:");
            long freeSpace = driveInfo.TotalFreeSpace;
            long totalSize = driveInfo.TotalSize;

            status.CurrentValue = "Free Disk Space: " + freeSpace.ToPrettySize() + " " + ((double) freeSpace/(double) totalSize).ToPrettyPercent();

            return status;

        }
    }
}
