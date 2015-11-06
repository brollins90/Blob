using System;
using System.Collections.Generic;
using Blob.Contracts.ServiceContracts;

namespace Before.Owin.Authorization
{
    public class BeforeAuthorizationMiddlewareOptions
    {
        public BeforeAuthorizationMiddlewareOptions()
        {
            ManagerProvider = (env) => null;
        }
        public IAuthorizationManagerService Manager { get; set; }
        public Func<IDictionary<string, object>, IAuthorizationManagerService> ManagerProvider { get; set; }
    }
}
