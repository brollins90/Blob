using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using Blob.Security.Authentication;
using Blob.Security.Authorization;
using Blob.Security.Sam;
using log4net;
using Ninject;
using Ninject.Extensions.Wcf;

namespace Blob.WcfHost.Infrastructure
{
    public class BlobHostFactory : NinjectServiceHostFactory
    {
        /// <summary>
        /// Create a ServiceHostFactory that supports Ninject DI
        /// </summary>
        public BlobHostFactory()
        {
            LogManager.GetLogger(typeof(BlobHostFactory)).Info("\n\n\n\nStarting host factory");

            StandardKernel kernel = new StandardKernel(new NinjectServiceModule());
            kernel.Bind<ServiceHost>().To<NinjectServiceHost>();
            SetKernel(kernel);
        }

        /// <summary>
        /// Create a WCF ServiceHost.  This is the primary registration point for non injectable components
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="baseAddresses"></param>
        /// <returns></returns>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            LogManager.GetLogger(typeof(BlobHostFactory)).Info("CreateServiceHost");

            ServiceHost host = base.CreateServiceHost(serviceType, baseAddresses);

            var col = new ReadOnlyCollection<IAuthorizationPolicy>(new IAuthorizationPolicy[] { new BlobUserAuthorizationPolicy() });
            ServiceAuthorizationBehavior sa = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
            if (sa == null)
            {
                sa = new ServiceAuthorizationBehavior();
                host.Description.Behaviors.Add(sa);
            }
            sa.ExternalAuthorizationPolicies = col;

            host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new BlobUserNamePasswordValidator();
            
            //host.Authentication.ServiceAuthenticationManager = new BlobServiceAuthenticationManager();
            host.Authorization.ServiceAuthorizationManager = new BlobServiceAuthorizationManager();


            return host;
        }
    }
}