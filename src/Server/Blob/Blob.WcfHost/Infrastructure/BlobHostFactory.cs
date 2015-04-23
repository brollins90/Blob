﻿using System;
using System.ServiceModel;
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
            //host.Authentication.ServiceAuthenticationManager = new BlobServiceAuthenticationManager();
            host.Authorization.ServiceAuthorizationManager = new BlobServiceAuthorizationManager();
            return host;
        }
    }
}