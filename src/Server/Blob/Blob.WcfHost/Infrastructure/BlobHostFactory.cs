using Ninject;
using Ninject.Extensions.Wcf;
using System.ServiceModel;

namespace Blob.WcfHost.Infrastructure
{
    public class BlobHostFactory : NinjectServiceHostFactory
    {
        public BlobHostFactory()
            : base()
        {
            var kernel = new StandardKernel(new NinjectServiceModule());
            kernel.Bind<ServiceHost>().To<NinjectServiceHost>();
            SetKernel(kernel);
        }
    }
}