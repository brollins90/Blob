using Behind.Service;
using log4net;
using Topshelf;
using Topshelf.Ninject;

namespace Behind
{
    public class Program
    {
        static void Main(string[] args)
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("Started Behind Program");

            //XmlConfigurator.ConfigureAndWatch(new FileInfo(".\\log4net.config"));
            log.Info("Creating service");
            var host = HostFactory.New(x =>
            {
                x.UseNinject(new BehindModule());

                x.Service<BehindService>(sc =>
                {
                    sc.ConstructUsingNinject(); //(name => new BehindService());

                    // the start and stop methods for the service
                    sc.WhenStarted(s => s.Start());
                    sc.WhenStopped(s => s.Stop());

                    // optional pause/continue methods if used
                    //sc.WhenPaused(s => s.Pause());
                    //sc.WhenContinued(s => s.Continue());

                    // optional, when shutdown is supported
                    //sc.WhenShutdown(s => s.Shutdown());


                });

                x.SetDescription("Runs background processes for the Blob service");
                x.SetDisplayName("BehindService");
                x.SetServiceName("BehindService");


                x.StartAutomatically(); // Start the service automatically

                x.RunAsLocalSystem();

                x.EnableServiceRecovery(rc =>
                {
                    rc.RestartService(1); // restart the service after 1 minute
                    //rc.RestartSystem(1, "System is restarting!"); // restart the system after 1 minute
                    //rc.RunProgram(1, "notepad.exe"); // run a program
                    //rc.SetResetPeriod(1); // set the reset interval to one day
                });

            });

            log.Info("Starting service");
            host.Run();
        }
    }
}
