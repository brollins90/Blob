using System.IO;
using BMonitor.Common.Extensions;
using BMonitor.Common.Interfaces;

namespace BMonitor.Monitors.Default
{
    public class FreeDiskSpace : IMonitor
    {
        public FreeDiskSpace()
        {
            // todo: create a config
        }

        public string Execute()
        {
            DriveInfo driveInfo = new DriveInfo(@"C:");
            long freeSpace = driveInfo.TotalFreeSpace;
            long totalSize = driveInfo.TotalSize;

            return "Free Disk Space: " + freeSpace.ToPrettySize() + " " + ((double)freeSpace / (double)totalSize).ToPrettyPercent();
        }
    }
}
