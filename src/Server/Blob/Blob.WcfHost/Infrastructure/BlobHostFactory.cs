using System;
using log4net;
using Ninject;
using Ninject.Extensions.Wcf;
using System.ServiceModel;
using Blob.Security;

namespace Blob.WcfHost.Infrastructure
{
    public class BlobHostFactory : NinjectServiceHostFactory
    {
        public BlobHostFactory()
        {
            LogManager.GetLogger(typeof(BlobHostFactory)).Info("\n\n\n\nStarting host factory");

            StandardKernel kernel = new StandardKernel(new NinjectServiceModule());
            kernel.Bind<ServiceHost>().To<NinjectServiceHost>();
            SetKernel(kernel);
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            LogManager.GetLogger(typeof(BlobHostFactory)).Info("CreateServiceHost");

            var host = base.CreateServiceHost(serviceType, baseAddresses);
            host.Authentication.ServiceAuthenticationManager = new ServiceAuthenticationManager();
            //host.Authorization.ServiceAuthorizationManager = new BlobServiceAuthorizationManager();
            return host;
        }
    }
}