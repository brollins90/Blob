using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMonitor.Service
{
    public static class StaticDiskSpaceMonitor
    {
        public static string Execute(string parameters)
        {
            //string output = "";

            //var verbosity = MonitorVerbosity.Summary;
            //// check if the -v is in there
            //int indexOfV = parameters.IndexOf("-v", StringComparison.OrdinalIgnoreCase);
            //if (indexOfV != -1)
            //{
            //    int afterDaV = int.Parse(parameters.Substring(indexOfV + 2, 1));
            //    verbosity = (MonitorVerbosity)afterDaV;
            //}

            DriveInfo driveInfo = new DriveInfo(@"C:");
            long freeSpace = driveInfo.TotalFreeSpace;
            long totalSize = driveInfo.TotalSize;

            return "Free Disk Space: " + freeSpace + " " + ((double)freeSpace / (double)totalSize);
        }
    }
}
