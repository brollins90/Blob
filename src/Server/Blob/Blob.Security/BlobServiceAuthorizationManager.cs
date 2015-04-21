using Blob.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using log4net;

namespace Blob.Security
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
            using (var context = new BlobDbContext())
            using (var userStore = new BlobUserStore(context))
            {
                using (var userManager = new BlobUserManager(userStore))
                {
                    var identity = operationContext.ServiceSecurityContext.PrimaryIdentity;
                    var user = userManager.FindByName(identity.Name);
                    if (user == null)
                    {
                        var msg = String.Format("Unknown Username {0} .", user.UserName);
                        Trace.TraceWarning(msg);
                        throw new FaultException(msg);
                    }

                    //Assign roles to the Principal property for runtime to match with PrincipalPermissionAttributes decorated on the service operation.
                    var roleNames = userManager.GetRoles(user.Id).ToArray();//users without any role assigned should then call operations not decorated by PrincipalPermissionAttributes
                    operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, roleNames);

                    return true;
                }
            }

        }
    }
}
