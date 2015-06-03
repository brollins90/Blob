using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public class PingMonitor : BaseMonitor
    {
        protected override string MonitorId { get { return MonitorName + RemoteDevice; } }
        protected override string MonitorName { get { return "PingMonitor"; } }
        protected override string MonitorDescription { get { return "Checks if the service can ping the remote device"; } }
        protected override string MonitorLabel { get { return string.Format("Ping {0}", RemoteDevice); } }

        public string RemoteDevice { get; set; }

        public PingMonitor() : this("blobservice.rritc.com") { }

        public PingMonitor(string remoteDevice)
        {
            RemoteDevice = remoteDevice;
        }

        public override ResultData Execute(bool collectPerfData = false)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(RemoteDevice);

            IPStatus executionValue = reply.Status;
            AlertLevel alertLevel = CheckAlertLevelPing(executionValue);

            string currentValueString = string.Empty;
            switch (alertLevel)
            {
                case AlertLevel.CRITICAL:
                    currentValueString = string.Format("{1}: {2} ({2})",
                        RemoteDevice,
                        "CRITICAL",
                        executionValue.ToString());
                    break;
                case AlertLevel.OK:
                    currentValueString = string.Format("{0}: {1} ({2})",
                        RemoteDevice,
                        "OK",
                        executionValue.ToString());
                    break;
                case AlertLevel.UNKNOWN:
                    currentValueString = string.Format("UNKNOWN");
                    break;
                case AlertLevel.WARNING:
                    currentValueString = string.Format("{0}: {1} ({2} {3} {4})",
                        RemoteDevice,
                        "WARNING",
                        executionValue.ToString(),
                        Operation,
                        base.Warning);
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
                UnitOfMeasure = "",//Unit.ShortName,
                Value = currentValueString
            };
            // todo: perf
            return result;
        }

        private AlertLevel CheckAlertLevelPing(IPStatus actual)
        {
            switch (actual)
            {
                case IPStatus.Success:
                    return AlertLevel.OK;
                case IPStatus.BadOption:
                case IPStatus.Unknown:
                    return AlertLevel.UNKNOWN;
                //case IPStatus.DestinationNetworkUnreachable:
                //case IPStatus.DestinationHostUnreachable:
                //case IPStatus.DestinationProtocolUnreachable:
                //case IPStatus.DestinationPortUnreachable:
                //case IPStatus.NoResources:
                //case IPStatus.HardwareError:
                //case IPStatus.PacketTooBig:
                //case IPStatus.TimedOut:
                //case IPStatus.BadRoute:
                //case IPStatus.TtlExpired:
                //case IPStatus.TtlReassemblyTimeExceeded:
                //case IPStatus.ParameterProblem:
                //case IPStatus.SourceQuench:
                //case IPStatus.BadDestination:
                //case IPStatus.DestinationUnreachable:
                //case IPStatus.TimeExceeded:
                //case IPStatus.BadHeader:
                //case IPStatus.UnrecognizedNextHeader:
                //case IPStatus.IcmpError:
                //case IPStatus.DestinationScopeMismatch:
                default:
                    return AlertLevel.CRITICAL;
            }
        }
    }
}
