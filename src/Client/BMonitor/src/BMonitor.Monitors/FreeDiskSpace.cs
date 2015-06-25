using System;
using System.Collections.Generic;
using System.IO;
using BMonitor.Common.Extensions;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public interface IFreeDiskSpace
    {
        double GetFreePercent();
        long GetTotalFreeSpace();
        long GetTotalSize();
    }

    public class FreeDiskSpaceDriveInfo : IFreeDiskSpace
    {
        DriveInfo _driveInfo;

        public FreeDiskSpaceDriveInfo(string driveLetter)
        {
            _driveInfo = new DriveInfo(driveLetter);
        }

        public double GetFreePercent() => Math.Round(((double)GetTotalFreeSpace() / (double)GetTotalSize()) * 100);
        public long GetTotalFreeSpace() => _driveInfo.TotalFreeSpace;
        public long GetTotalSize() => _driveInfo.TotalSize;
    }

    public class FreeDiskSpace : BaseMonitor
    {
        protected override string MonitorId => MonitorName + DriveLetter;
        protected override string MonitorName => "FreeDiskSpace";
        protected override string MonitorDescription => "Checks the amount of disk space available";
        protected override string MonitorLabel => $"Disk {DriveLetter.ToUpperInvariant()} - {DriveDescription}";

        private string _driveLetter = "C";
        public string DriveLetter { get { return _driveLetter; } set { _driveLetter = $"{value.ToUpperInvariant()}:"; } }
        public string DriveDescription { get; set; } = "OS-default";


        public override ResultData Execute(bool collectPerfData = false)
        {
            IFreeDiskSpace _diskSpace = new FreeDiskSpaceDriveInfo(DriveLetter);

            double executionValue = _diskSpace.GetFreePercent();
            AlertLevel alertLevel = base.CheckAlertLevel(executionValue);

            string currentValueString = string.Empty;
            switch (alertLevel)
            {
                case AlertLevel.CRITICAL:
                    currentValueString = $"{DriveLetter} ({DriveDescription}): {executionValue}% left ({_diskSpace.GetTotalFreeSpace().BytesToGb()}GB/{_diskSpace.GetTotalSize().BytesToGb()}GB) ({Operation.ShortString}{Critical}%) : CRITICAL";
                    break;
                case AlertLevel.OK:
                    currentValueString = $"{executionValue}% free space ({_diskSpace.GetTotalSize().BytesToGb()}GB) : OK";
                    break;
                case AlertLevel.UNKNOWN:
                    currentValueString = "UNKNOWN";
                    break;
                case AlertLevel.WARNING:
                    currentValueString = $"{DriveLetter} ({DriveDescription}): {executionValue}% left ({_diskSpace.GetTotalFreeSpace().BytesToGb()}GB/{_diskSpace.GetTotalSize().BytesToGb()}GB) ({Operation.ShortString}{Critical}%) : WARNING";
                    break;
            }

            ResultData result = new ResultData()
            {
                AlertLevel = alertLevel,
                MonitorDescription = MonitorDescription,
                MonitorId = MonitorId,
                MonitorLabel = MonitorLabel,
                MonitorName = MonitorName,
                Perf = new List<PerformanceData>(),
                TimeGenerated = DateTime.Now,
                UnitOfMeasure = "%",
                Value = currentValueString
            };

            if (collectPerfData)
            {
                long criticalBytesLeft = (long)((double)_diskSpace.GetTotalSize() * (Critical / 100));
                long warningBytesLeft = (long)((double)_diskSpace.GetTotalSize() * (Warning / 100));

                // also we want to invert the numbers in the results
                PerformanceData perf = new PerformanceData
                {
                    Critical = $"{(_diskSpace.GetTotalSize() - criticalBytesLeft)}",
                    Label = DriveLetter,
                    Max = $"{_diskSpace.GetTotalSize()}",
                    Min = "0",
                    UnitOfMeasure = "B",
                    Value = $"{_diskSpace.GetTotalFreeSpace()}",
                    Warning = $"{(_diskSpace.GetTotalSize() - warningBytesLeft)}",
                };
                result.Perf.Add(perf);

            }
            return result;
        }
    }
}
