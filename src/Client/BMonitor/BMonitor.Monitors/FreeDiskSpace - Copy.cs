//using BMonitor.Common.Interfaces;
//using System;
//using System.IO;
//using BMonitor.Common;
//using BMonitor.Common.Extensions;

//namespace BMonitor.Monitors
//{

//    // perf
//    // T0_System_Board_Ambient=15C;42;47 W2_System_Board_System_Level=238W;917;966 A0_PS_1_Current=0.8A;0;0 A1_PS_2_Current=0.8A;0;0 V20_PS_1_Voltage=120V;0;0 V21_PS_2_Voltage=120V;0;0 F0_System_Board_FAN_1=3600rpm;0;0 F1_System_Board_FAN_2=3600rpm;0;0 F2_System_Board_FAN_3=3600rpm;0;0 F3_System_Board_FAN_4=3600rpm;0;0 F4_System_Board_FAN_5=3600rpm;0;0

//    // 'label'=value[UOM];[warn];[crit];[min];[max]
//    // 'F:'=1697GB;1490;1677;0;1863

//    //public class FreeDiskSpace : BaseMonitor<decimal> { }

//    public class FreeDiskSpace : IMonitor
//    {
//        private const string DESCRIPTION_TEMPLATE = "Disk {0} - {1}";
//        private const string CRITICAL_TEMPLATE = "{0}: {1} ({2}): {3}% left ({4}GB/{5}GB) (<{6}%) : CRITICAL";
//        private const string WARNING_TEMPLATE = "{0}: {1} ({2}): {3}% left ({4}GB/{5}GB) (<{6}%) : WARNING";
//        private const string OK_TEMPLATE = "{0}% free space ({1}GB) : OK";
//        private const string UNKNOWN_TEMPLATE = "UNKNOWN";

//        private const string PERF_TEMPLATE = "'{0}:'={1}{2};{3};{4};{5};{6}";

//        private readonly string _driveLetter;
//        private readonly string _driveDescription;
//        private readonly UnitOfMeasure _unitOfMeasure;
//        private readonly long _warningLevel;
//        private readonly long _criticalLevel;


//        public FreeDiskSpace(string driveLetter, string driveDescription, UnitOfMeasure unitOfMeasure = UnitOfMeasure.PERCENT, long warningLevel = 20, long criticalLevel = 10)
//        {
//            _driveLetter = driveLetter;
//            _driveDescription = driveDescription;
//            _unitOfMeasure = unitOfMeasure;
//            _warningLevel = 20;
//            _criticalLevel = 10;
//        }

//        public ResultData Execute(bool collectPerfData = false)
//        {
//            ResultData result = new ResultData()
//            {
//                MonitorDescription = string.Format(DESCRIPTION_TEMPLATE, _driveLetter.ToUpper(), _driveDescription),
//                MonitorName = "FreeDiskSpace",
//                TimeGenerated = DateTime.Now,
//                UnitOfMeasure = _unitOfMeasure
//            };
//            PerformanceData perf = new PerformanceData
//            {
//                Critical = _criticalLevel.ToString(),
//                Label = _driveLetter.ToUpper() + ":",
//                //UnitOfMeasure = UnitOfMeasure.GigaByte,
//                Value = null,
//                Warning = _warningLevel.ToString()
//            };

//            try
//            {
//                DriveInfo driveInfo = new DriveInfo(string.Format("{0}:", _driveLetter.ToUpper()));
//                long freeSpace = driveInfo.TotalFreeSpace;
//                long totalSize = driveInfo.TotalSize;
//                double freePercent = Math.Round(((double)freeSpace / (double)totalSize) * 100);

//                double totalGb = totalSize.BytesToGb();
//                perf.Value = (totalSize - freeSpace).BytesToGb().ToString();
//                perf.Warning = (totalGb * ((100 - _warningLevel) * 0.01)).ToString();
//                perf.Critical = (totalGb * ((100 - _criticalLevel) * 0.01)).ToString();
//                perf.Min = "0";
//                perf.Max = totalGb.ToString();

//                double testValue = (_unitOfMeasure == UnitOfMeasure.PERCENT) ? freePercent
//                    : (_unitOfMeasure == UnitOfMeasure.B) ? freeSpace
//                    : (_unitOfMeasure == UnitOfMeasure.KB) ? freeSpace.BytesToKb()
//                    : (_unitOfMeasure == UnitOfMeasure.MB) ? freeSpace.BytesToMb()
//                    : (_unitOfMeasure == UnitOfMeasure.GB) ? freeSpace.BytesToGb()
//                    : (_unitOfMeasure == UnitOfMeasure.TB) ? freeSpace.BytesToTb()
//                    : 0; // undefined UnitOfMeasure

//                if (testValue <= _criticalLevel)
//                {
//                    result.Value = string.Format(CRITICAL_TEMPLATE,
//                                                        driveInfo.Name,
//                                                        driveInfo.VolumeLabel,
//                                                        _driveDescription,
//                                                        freePercent,
//                                                        freeSpace.BytesToGb(),
//                                                        totalSize.BytesToGb(),
//                                                        _criticalLevel);
//                    result.AlertLevel = AlertLevel.CRITICAL;
//                }
//                else if (testValue <= _warningLevel)
//                {
//                    result.Value = string.Format(WARNING_TEMPLATE,
//                                                        driveInfo.Name,
//                                                        driveInfo.VolumeLabel,
//                                                        _driveDescription,
//                                                        freePercent,
//                                                        freeSpace.BytesToGb(),
//                                                        totalSize.BytesToGb(),
//                                                        _warningLevel);
//                    result.AlertLevel = AlertLevel.WARNING;
//                }
//                else
//                {
//                    result.Value = string.Format(OK_TEMPLATE,
//                                                        freePercent,
//                                                        freeSpace.BytesToGb());
//                    result.AlertLevel = AlertLevel.OK;
//                }
//            }
//            catch (Exception e)
//            {
//                //todo: handle the exception
//                result.Value = string.Format(UNKNOWN_TEMPLATE);
//                result.AlertLevel = AlertLevel.UNKNOWN;
//            }

//            if (collectPerfData)
//                result.Perf.Add(perf);

//            return result;
//        }
//    }
//}
