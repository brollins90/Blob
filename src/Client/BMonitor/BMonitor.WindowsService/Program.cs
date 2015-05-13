using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using BMonitor.Service;
using BMonitor.Service.Infrastructure;
using log4net;
using Ninject;

namespace BMonitor.WindowsService
{
    public static class Program
    {
        static readonly ManualResetEvent CancelKeyStopEvent = new ManualResetEvent(false);

        public static void Main(string[] args)
        {
            ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            ProgramSettings settings = ParseCommandLine(args);

            IKernel kernel = new StandardKernel();
            kernel.Load(new DependencyInjection());

            MonitorService winServiceWrapper = kernel.Get<MonitorService>();

            if (settings.RunAsConsole)
            {
                _log.Info("Starting service...");
                Console.WriteLine("Starting service...");
                winServiceWrapper.Start(args);
                Console.CancelKeyPress += (o, e) =>
                {
                    e.Cancel = true;
                    CancelKeyStopEvent.Set();
                };

                _log.Info("Press Ctrl+C or Ctrl+Break to quit");
                Console.WriteLine("Press Ctrl+C or Ctrl+Break to quit");
                CancelKeyStopEvent.WaitOne();

                _log.Info("Stopping service...");
                Console.WriteLine("Stopping service...");
                winServiceWrapper.Stop();
            }
            else
            {
                // have the SCM run the service for us
                _log.Info("Passing MonitorService to the SCM.");
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
