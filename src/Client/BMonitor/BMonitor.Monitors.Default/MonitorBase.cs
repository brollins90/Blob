//using BMonitor.Common.Interfaces;
//using System;
//using System.IO;
//using BMonitor.Common.Extensions;

//namespace BMonitor.Monitors.Default
//{

//    // perf
//    // T0_System_Board_Ambient=15C;42;47 W2_System_Board_System_Level=238W;917;966 A0_PS_1_Current=0.8A;0;0 A1_PS_2_Current=0.8A;0;0 V20_PS_1_Voltage=120V;0;0 V21_PS_2_Voltage=120V;0;0 F0_System_Board_FAN_1=3600rpm;0;0 F1_System_Board_FAN_2=3600rpm;0;0 F2_System_Board_FAN_3=3600rpm;0;0 F3_System_Board_FAN_4=3600rpm;0;0 F4_System_Board_FAN_5=3600rpm;0;0

//    // 'label'=value[UOM];[warn];[crit];[min];[max]
//    // 'F:'=1697GB;1490;1677;0;1863

//    public class Threshold<TUoM, TVal>
//    {
//        public string Unit { get; set; }
//        public TVal Warning { get; set; }
//        public TVal Critical { get; set; }

//        public Threshold(TVal warningLevel, TVal criticalLevel, string unitString = null)
//        {
//            Unit = unitString ?? TUoM
//            Warning = warningLevel;
//            Critical = criticalLevel;
//        }
//    }

//    public abstract class MonitorBase : IMonitor
//    {
//        protected string DescriptionTemplate { get; set; }
//        protected string CriticalTemplate { get; set; }
//        protected string WarningTemplate { get; set; }
//        protected string OkTemplate { get; set; }
//        protected string UnknownTemplate { get; set; }
//        protected string PerfTemplate { get; set; }

//        protected Threshold 


//        private readonly string _driveLetter;
//        private readonly string _driveDescription;
//        private readonly UnitOfMeasure _unitOfMeasure;
//        private readonly int _warningLevel;
//        private readonly int _criticalLevel;


//        public FreeDiskSpace(string driveLetter, string driveDescription, UnitOfMeasure unitOfMeasure = UnitOfMeasure.PERCENT, int warningLevel = 20, int criticalLevel = 10)
//        {
//            _driveLetter = driveLetter;
//            _driveDescription = driveDescription;
//            _unitOfMeasure = unitOfMeasure;
//            _warningLevel = warningLevel;
//            _criticalLevel = criticalLevel;
//        }




//        public MonitorResult Execute(bool collectPerfData = false)
//        {
//            MonitorResult result = new MonitorResult()
//            {
//                MonitorDescription = string.Format(DescriptionTemplate, _driveLetter.ToUpper(), _driveDescription),
//                MonitorName = "FreeDiskSpace",
//                TimeGenerated = DateTime.Now,
//                UnitOfMeasure = _unitOfMeasure
//            };
//            MonitorPerf perf = new MonitorPerf
//            {
//                // 'label'=value[UOM];[warn];[crit];[min];[max]
//                Critical = _criticalLevel.ToString(),
//                Label = _driveLetter.ToUpper(),
//                Max = null, // the largest a %value can be (not required for %)
//                Min = "0", // the smallest a %value can be (not required for %)
//            };

//            try
//            {
//                DriveInfo driveInfo = new DriveInfo(string.Format("{0}:", _driveLetter.ToUpper()));
//                long freeSpace = driveInfo.TotalFreeSpace;
//                long totalSize = driveInfo.TotalSize;
//                double freePercent = Math.Round(((double)freeSpace / (double)totalSize) * 100);

//                double testValue = (_unitOfMeasure == UnitOfMeasure.PERCENT) ? freePercent
//                    : (_unitOfMeasure == UnitOfMeasure.B) ? freeSpace
//                    : (_unitOfMeasure == UnitOfMeasure.KB) ? freeSpace.BytesToKb()
//                    : (_unitOfMeasure == UnitOfMeasure.MB) ? freeSpace.BytesToMb()
//                    : (_unitOfMeasure == UnitOfMeasure.GB) ? freeSpace.BytesToGb()
//                    : (_unitOfMeasure == UnitOfMeasure.TB) ? freeSpace.BytesToTb()
//                    : 0; // undefined UnitOfMeasure

//                if (testValue <= _criticalLevel)
//                {
//                    result.Value = string.Format(CriticalTemplate,
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
//                    result.Value = string.Format(WarningTemplate,
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
//                    result.Value = string.Format(OkTemplate,
//                                                        freePercent,
//                                                        freeSpace.BytesToGb());
//                    result.AlertLevel = AlertLevel.OK;
//                }
//            }
//            catch (Exception e)
//            {
//                //todo: handle the exception
//                result.Value = string.Format(UnknownTemplate);
//                result.AlertLevel = AlertLevel.UNKNOWN;
//            }

//            return result;
//        }
//    }
//}
