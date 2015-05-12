using System;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Description;
using Blob.Security.Tokens;
using log4net;
using Ninject;
using Ninject.Extensions.Wcf;

namespace Blob.WcfHost.Infrastructure
{
    public class BlobHostFactory : NinjectServiceHostFactory
    {
        private readonly ILog _log;

        /// <summary>
        /// Create a ServiceHostFactory that supports Ninject DI
        /// </summary>
        public BlobHostFactory()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Info("\n\n\n\nStarting host factory");

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
            _log.Debug("CreateServiceHost");

            ServiceHost host = base.CreateServiceHost(serviceType, baseAddresses);

            // http://leastprivilege.com/2012/07/16/wcf-and-identity-in-net-4-5-usernamepassword-authentication/
            // and plural site
            host.Credentials.UseIdentityConfiguration = true;

            var idConfig = CreateIdentityConfiguration(host.Credentials);
            host.Credentials.IdentityConfiguration = idConfig;

            return host;
        }

        /// <summary>
        /// Create the Security token handler in the IdentityConfiguration and returns the completed section
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        private IdentityConfiguration CreateIdentityConfiguration(ServiceCredentials credentials)
        {
            var idConfig = new IdentityConfiguration();

            idConfig.SecurityTokenHandlers.AddOrReplace(
                new GenericUserNameSecurityTokenHandler(
                    (uname, password) =>
                    {
                        var validator = credentials.UserNameAuthentication.CustomUserNamePasswordValidator;
                        try
                        {
                            validator.Validate(uname, password);
                            return true;
                        }
                        catch (SecurityTokenValidationException)
                        {
                            return true;
                        }
                    }));
            return idConfig;
        }
    }
}
