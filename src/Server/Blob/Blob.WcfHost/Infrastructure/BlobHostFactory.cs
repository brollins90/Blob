using log4net;
using Ninject;
using Ninject.Extensions.Wcf;
using System.ServiceModel;

namespace Blob.WcfHost.Infrastructure
{
    public class BlobHostFactory : NinjectServiceHostFactory
    {
        public BlobHostFactory()
        {
            LogManager.GetLogger(typeof(BlobHostFactory)).Info("Starting host factory");

            StandardKernel kernel = new StandardKernel(new NinjectServiceModule());
            kernel.Bind<ServiceHost>().To<NinjectServiceHost>();
            SetKernel(kernel);
        }
    }
}