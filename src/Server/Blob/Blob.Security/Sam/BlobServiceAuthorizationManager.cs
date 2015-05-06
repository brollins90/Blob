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
            
            IIdentity identity = operationContext.ServiceSecurityContext.PrimaryIdentity;

            Guid g;
            if (Guid.TryParse(identity.Name, out g))
            {
                // assume this is a device and not a user
                _log.Debug("Found a device.  add the device role and return"); 
                operationContext.ServiceSecurityContext.AuthorizationContext
                             .Properties["Principal"] = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, new [] {"Device"});

                return true;
            }

            using (BlobDbContext context = new BlobDbContext())
            {
                using (BlobUserStore userStore = new BlobUserStore(context))
                {
                    using (BlobUserManager userManager = new BlobUserManager(userStore))
                    {
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

        //protected void logRoles(OperationContext operationContext)
        //{
        //    var p = operationContext.ServiceSecurityContext.AuthorizationContext
        //                            .Properties["Principal"];
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("bsam - ");
        //    sb.Append(p.)
            
        //}

    }
}

//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.ServiceModel;
//using System.Threading;
//using Blob.Core.Domain;
//using Blob.Data;
//using Blob.Data.Identity;
//using log4net;

//namespace Blob.Security.Sam
//{
//    public class BlobServiceAuthorizationManager : ServiceAuthorizationManager
//    {
//        private readonly ILog _log;

//        public BlobServiceAuthorizationManager()
//        {
//            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
//            _log.Debug("Constructing BlobServiceAuthorizationManager");
//        }

//        protected override bool CheckAccessCore(OperationContext operationContext)
//        {
//            _log.Debug("CheckAccessCore");
//            ClaimsIdentity identity = operationContext.ServiceSecurityContext.PrimaryIdentity;
//            List<Claim> claimsCollection = new List<Claim>();

//            using (BlobDbContext context = new BlobDbContext())
//            {

//                Guid g;
//                if (Guid.TryParse(identity.Name, out g))
//                {
//                    // assume this is a device and not a user
//                    _log.Debug("Found a device.  add the device role and return");

//                    //ClaimsPrincipal newPrincipal = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, new[] { "Device" });

//                    claimsCollection.Add(new Claim(ClaimTypes.Role, "Device"));
//                    ClaimsPrincipal newPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claimsCollection, "Customer"));
//                    operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = newPrincipal;
//                    return true;
//                }

//                using (BlobUserStore userStore = new BlobUserStore(context))
//                {
//                    using (BlobUserManager userManager = new BlobUserManager(userStore))
//                    {
//                        User user = userManager.FindByNameAsync2(identity.Name).Result;
//                        _log.Debug("Got user: " + user);

//                        if (user == null)
//                        {
//                            string msg = String.Format("Unknown Username {0} .", identity.Name);
//                            Trace.TraceWarning(msg);
//                            _log.Debug(msg);
//                            throw new FaultException(msg);
//                        }

//                        //Assign roles to the Principal property for runtime to match with PrincipalPermissionAttributes decorated on the service operation.
//                        string[] roleNames = userManager.GetRolesAsync(user.Id).Result.ToArray(); //users without any role assigned should then call operations not decorated by PrincipalPermissionAttributes

//                        ClaimsPrincipal p = new ClaimsPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity);

//                        //claimsCollection.Add(new Claim(ClaimTypes.Role, "Device"));
//                        //ClaimsPrincipal newPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claimsCollection, "Customer"));
//                        //operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = newPrincipal;

//                        operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] =
//                            //new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, roleNames);
//                            new ClaimsPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity);


//                        //add customer id 

//                        ClaimsIdentity identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
//                        //identity.AddClaim(new Claim())
//                        return true;
//                    }
//                }

//            }
//        }

//        //protected void logRoles(OperationContext operationContext)
//        //{
//        //    var p = operationContext.ServiceSecurityContext.AuthorizationContext
//        //                            .Properties["Principal"];
//        //    StringBuilder sb = new StringBuilder();
//        //    sb.Append("bsam - ");
//        //    sb.Append(p.)

//        //}

//    }
//}