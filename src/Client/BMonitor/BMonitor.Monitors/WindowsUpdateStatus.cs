//using System;
//using System.Collections.Generic;
//using System.IO;
//using BMonitor.Common;
//using BMonitor.Common.Extensions;
//using BMonitor.Common.Models;

//namespace BMonitor.Monitors
//{
//    public class FreeDiskSpace : BaseMonitor
//    {
//        private readonly string _driveLetter;
//        private readonly string _driveDescription;

//        public FreeDiskSpace(string driveLetter, string driveDescription)
//        {
//            _driveLetter = driveLetter;
//            _driveDescription = driveDescription;
//        }

//        protected override string MonitorName { get { return "FreeDiskSpace"; } }
//        protected override string MonitorDescription { get { return "FreeDiskSpace Description"; } }


//        public override ResultData Execute(bool collectPerfData = false)
//        {
//            string driveLetter = string.Format("{0}:", _driveLetter.ToUpper());
//            string driveName;
//            string driveLabel;
//            long totalFreeSpace;
//            long totalSize;
//            double freePercent;
//            double executionValue;

//            DriveInfo driveInfo = new DriveInfo(driveLetter);
//            driveName = driveInfo.Name;
//            driveLabel = driveInfo.VolumeLabel;
//            totalFreeSpace = driveInfo.TotalFreeSpace;
//            totalSize = driveInfo.TotalSize;
//            freePercent = Math.Round(((double)totalFreeSpace / (double)totalSize) * 100);
            
//            executionValue = freePercent;


//            AlertLevel alertLevel = MonitorThreshold.CheckAlertLevel(executionValue);

//            string currentValueString = string.Empty;
//            switch (alertLevel)
//            {
//                case AlertLevel.CRITICAL:
//                    currentValueString = string.Format("{0}: {1} ({2}): {3}% left ({4}GB/{5}GB) (<{6}%) : CRITICAL", 
//                        driveName,
//                        driveLabel,
//                        _driveDescription,
//                        freePercent,
//                        totalFreeSpace.BytesToGb(),
//                        totalSize.BytesToGb(),
//                        MonitorThreshold.Critical.Low); // is this the correct value to display
//                    break;
//                case AlertLevel.OK:
//                    currentValueString = string.Format("{0}% free space ({1}GB) : OK", 
//                        freePercent,
//                        totalFreeSpace.BytesToGb());
//                    break;
//                case AlertLevel.UNKNOWN:
//                    currentValueString = string.Format("UNKNOWN");
//                    break;
//                case AlertLevel.WARNING:
//                    currentValueString = string.Format("{0}: {1} ({2}): {3}% left ({4}GB/{5}GB) (<{6}%) : WARNING", 
//                        driveName,
//                        driveLabel,
//                        _driveDescription,
//                        freePercent,
//                        totalFreeSpace.BytesToGb(),
//                        totalSize.BytesToGb(),
//                        MonitorThreshold.Warning.Low); // is this the correct value to display
//                    break;
//            }

//            ResultData result = new ResultData()
//                                {
//                                    AlertLevel = alertLevel,
//                                    MonitorDescription = MonitorDescription,
//                                    MonitorName = MonitorName,
//                                    Perf = new List<PerformanceData>(),
//                                    TimeGenerated = DateTime.Now,
//                                    UnitOfMeasure = Unit.ShortName,
//                                    Value = currentValueString
//                                };

//            if (collectPerfData)
//            {
//                // perfdata for this monitor is always in GB
//                long crit = (MonitorThreshold.Critical.Percent)
//                                ? ((long)(MonitorThreshold.Critical.Limit * .01d * totalSize))
//                                : ((long)(MonitorThreshold.Critical.Limit));
//                long warn = (MonitorThreshold.Warning.Percent)
//                                ? ((long)(MonitorThreshold.Warning.Limit * .01d * totalSize))
//                                : ((long)(MonitorThreshold.Warning.Limit));

//                // also we want to invert the numbers in the results
//                PerformanceData perf = new PerformanceData
//                                       {
//                                           Critical = (totalSize - crit).BytesToGb().ToString(),
//                                           Label = _driveLetter,
//                                           Max = totalSize.BytesToGb().ToString(), // the largest a %value can be (not required for %)
//                                           Min = "0", // the smallest a %value can be (not required for %)
//                                           UnitOfMeasure = "GB",
//                                           Value = totalFreeSpace.BytesToGb().ToString(),
//                                           Warning = (totalSize - warn).BytesToGb().ToString()
//                                       };
//                result.Perf.Add(perf);

//            }
//            return result;
//        }

//        //public string GetFreeSpace()
//        //{
//        //    ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
//        //    disk.Get();
//        //    string freespace = disk["FreeSpace"].ToString();
//        //    Console.WriteLine(freespace);
//        //    return freespace;
//        //}
//    }
//}
