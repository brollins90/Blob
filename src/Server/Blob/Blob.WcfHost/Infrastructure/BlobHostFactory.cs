using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Policy;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using Blob.Security.Authentication;
using Blob.Security.Authorization;
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
            //FederatedAuthentication.FederationConfigurationCreated += FederatedAuthentication_FederationConfigurationCreated;
            
            ServiceHost host = base.CreateServiceHost(serviceType, baseAddresses);
            
            ////FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthorizationManager;
            //host.Credentials.IdentityConfiguration = new IdentityConfiguration();
            //host.Credentials.IdentityConfiguration.ClaimsAuthorizationManager = new MyClaimsAuthorizationManager();

            var authorizationPolicies = new ReadOnlyCollection<IAuthorizationPolicy>(new IAuthorizationPolicy[] { new BlobUserAuthorizationPolicy() });
            ServiceAuthorizationBehavior behaviors = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
            if (behaviors == null)
            {
                behaviors = new ServiceAuthorizationBehavior();
                host.Description.Behaviors.Add(behaviors);
            }
            behaviors.ExternalAuthorizationPolicies = authorizationPolicies;
            

            host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new BlobUserNamePasswordValidator();

            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host.Authorization.ServiceAuthorizationManager = new BlobServiceAuthorizationManager();

            //host.Authentication.ServiceAuthenticationManager = new BlobServiceAuthenticationManager();
            return host;
        }
        //void FederatedAuthentication_FederationConfigurationCreated(object sender, System.IdentityModel.Services.Configuration.FederationConfigurationCreatedEventArgs e)
        //{
        //    //var cam = DependencyResolver.Current.GetService<ClaimsAuthenticationManager>();  // Instantiate your implementation here using any IoC you want.
        //    //e.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager = new MyClaimsAuthenticationManager();
        //    //e.FederationConfiguration.IdentityConfiguration.ClaimsAuthorizationManager = new BlobClaimsAuthorizationManager();
        //}


    }
}
