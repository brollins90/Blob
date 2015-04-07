using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;

namespace BMonitor.Service
{
    public static class Program
    {
        static readonly ManualResetEvent CancelKeyStopEvent = new ManualResetEvent(false);

        public static void Main(string[] args)
        {
            ProgramSettings settings = ParseCommandLine(args);

            MonitorService winServiceWrapper = new MonitorService();

            if (settings.RunAsConsole)
            {
                Console.WriteLine("Starting service...");
                winServiceWrapper.Start(args);
                Console.CancelKeyPress += (o, e) =>
                {
                    e.Cancel = true;
                    CancelKeyStopEvent.Set();
                };

                Console.WriteLine("Press Ctrl+C or Ctrl+Break to quit");
                CancelKeyStopEvent.WaitOne();

                Console.WriteLine("Stopping service...");
                winServiceWrapper.Stop();
            }
            else
            {
                // have the SCM run the service for us
                ServiceBase.Run(winServiceWrapper);
            }
        }

        internal static ProgramSettings ParseCommandLine(IEnumerable<string> args)
        {
            ProgramSettings settings = new ProgramSettings();

            if (args != null)
            {
                foreach (var arg in args)
                {
                    string argClean = arg.TrimStart('-', '/');

                    if (String.Compare(argClean, "console", StringComparison.OrdinalIgnoreCase) == 0)
                        settings.RunAsConsole = true;
                }
            }

            return settings;
        }

        internal sealed class ProgramSettings
        {
            public bool RunAsConsole { get; set; }
        }
    }
}
