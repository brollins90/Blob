using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Data.Identity;
using log4net;

namespace Blob.Security.Sam
{
    public class BlobServiceAuthorizationManager : ServiceAuthorizationManager
    {
        private readonly ILog _log;

        public BlobServiceAuthorizationManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobServiceAuthorizationManager");
        }

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            _log.Debug("CheckAccessCore");
            using (BlobDbContext context = new BlobDbContext())
            {
                using (BlobUserStore userStore = new BlobUserStore(context))
                {
                    using (BlobUserManager userManager = new BlobUserManager(userStore))
                    {
                        IIdentity identity = operationContext.ServiceSecurityContext.PrimaryIdentity;
                        User user = userManager.FindByNameAsync2(identity.Name).Result;
                        _log.Debug("Got user: " + user);

                        if (user == null)
                        {
                            string msg = String.Format("Unknown Username {0} .", identity.Name);
                            Trace.TraceWarning(msg);
                            _log.Debug(msg);
                            throw new FaultException(msg);
                        }

                        //Assign roles to the Principal property for runtime to match with PrincipalPermissionAttributes decorated on the service operation.
                        string[] roleNames = userManager.GetRolesAsync(user.Id).Result.ToArray(); //users without any role assigned should then call operations not decorated by PrincipalPermissionAttributes
                        operationContext.ServiceSecurityContext.AuthorizationContext
                            .Properties["Principal"] = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, roleNames);
                        return true;
                    }
                }

            }
        }
    }
}