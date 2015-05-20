using log4net;
using Ninject;
using Ninject.Extensions.Quartz;
using Quartz;
using Quartz.Impl;

namespace Behind.Service
{
    public class BehindService
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;
        private ISchedulerFactory _sf;
        private IScheduler _sched;

        public BehindService(IKernel kernel, ILog log)
        {
            _kernel = kernel;
            _log = log;
        }

        public void Start()
        {
            _log.Info("BehindService is Started");

            _sf = new StdSchedulerFactory();
            _sched = _sf.GetScheduler();

            //LoadJobs(_sched);

            _sched.Start();
            _sched.JobFactory = new NinjectJobFactory(_kernel);
        }

        public void Stop()
        {
            _log.Info("SampleService is Stopped");

            _sched.Shutdown(waitForJobsToComplete: true);
        }

        //public static void LoadJobs(IScheduler sched)
        //{
        //    //IJobDetail job = JobBuilder.Create()
        //    //                .WithIdentity("job1", "group1")
        //    //                .Build();

        //    //ITrigger trigger = TriggerBuilder.Create()
        //    //    .WithIdentity("trigger1", "group1")
        //    //    .ForJob(job)
        //    //    .StartNow()
        //    //    .WithSimpleSchedule(x => x.RepeatForever().WithIntervalInHours(1))
        //    //    .Build();

        //    //sched.ScheduleJob(job, trigger);
        //}


        //readonly private CancellationTokenSource _cancellationTokenSource;
        //readonly private CancellationToken _cancellationToken;
        //readonly private Task _task;

        //public BehindService()
        //{
        //    _cancellationTokenSource = new CancellationTokenSource();
        //    _cancellationToken = _cancellationTokenSource.Token;

        //    _task = new Task(DoWork, _cancellationToken);
        //}

        //public void Start()
        //{
        //    _task.Start();
        //}
        //public void Stop()
        //{
        //    _cancellationTokenSource.Cancel();
        //    _task.Wait(_cancellationToken);
        //}

        //private void DoWork()
        //{
        //    while (!_cancellationTokenSource.IsCancellationRequested)
        //    {

        //    }
        //}
    }
}
