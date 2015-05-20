using System.Collections.Generic;
using Ninject;
using Quartz;

namespace Behind.Core
{
    public class BehindManager : IBehind
    {
        //private readonly JobManager _jobManager;
        //private readonly List<IScheduler> _schedulers = new List<IScheduler>();

        private IScheduler _scheduler;


        private readonly IKernel _kernel;

        public BehindManager(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Start()
        {
            
        }

        public void Dispose()
        {
        }
    }
}
