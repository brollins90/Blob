using System;
using System.IdentityModel.Claims;
using System.Linq;
using System.ServiceModel;
using log4net;

namespace Blob.Security.Authorization
{
    // https://msdn.microsoft.com/en-us/library/ms751416.aspx
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
            //string action = operationContext.RequestContext.RequestMessage.Headers.Action;
            string action = GetActionMethodInfo(operationContext).Name;
            Console.WriteLine("action: {0}", action);

            foreach (ClaimSet claimSet in operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets)
            {
                // Examine only those claim sets issued by System. 
                if (claimSet.Issuer == ClaimSet.System)
                {
                    foreach (Claim c in claimSet.FindClaims("http://www.contoso.com/claims/allowedoperation", Rights.PossessProperty))
                    {
                        Console.WriteLine("resource: {0}", c.Resource.ToString());
                        if (action == c.Resource.ToString())
                            return true;
                    }
                }
            }

            // If this point is reached, return false to deny access. 
            return false;
        }

        //protected override bool CheckAccessCore(OperationContext operationContext)
        //{
        //    var action = GetActionMethodInfo(operationContext);

        //    return true;
        //}

        //http://stackoverflow.com/a/10025331/148256
        ///<summary>Returns the Method info for the method (OperationContract) that is called in this WCF request.</summary>
        System.Reflection.MethodInfo GetActionMethodInfo(System.ServiceModel.OperationContext operationContext)
        {
            string bindingName = operationContext.EndpointDispatcher.ChannelDispatcher.BindingName;
            string methodName;
            if (bindingName.Contains("WebHttpBinding"))
            {
                //REST request
                methodName = (string)operationContext.IncomingMessageProperties["HttpOperationName"];
            }
            else
            {
                //SOAP request
                string action = operationContext.IncomingMessageHeaders.Action;
                methodName = operationContext.EndpointDispatcher.DispatchRuntime.Operations.FirstOrDefault(o => o.Action == action).Name;
            }
            // Insert your own error-handling here if (operation == null)
            Type hostType = operationContext.Host.Description.ServiceType;
            return hostType.GetMethod(methodName);
        }

        //public override bool CheckAccess(AuthorizationContext context)
        //{
        //    string resourceStr = context.Resource.First().Value;
        //    string actionStr = context.Action.First().Value;


        //    //int resourceId;
        //    //Operations operationId;
        //    //try { resourceId = Int32.Parse(resourceStr); }
        //    //catch { throw new Exception("Invalid resource. Must be a string representation of an integer value."); }
        //    //try { operationId = (Operations)Enum.Parse(typeof(Operations), actionStr); }
        //    //catch { throw new Exception("Invalid action/operation. Must be a string representation of an integer value."); }

        //    //Get the current claims principal
        //    var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;
        //    //Make sure they are authenticated
        //    if (!prinicpal.Identity.IsAuthenticated)
        //        return false;
        //    //Get the roles from the claims
        //    var roles = prinicpal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
        //    //Check if they are authorized
        //    //return ResourceService.Authorize(resourceId, operationId, roles);

        //}

        //private string[] getRoles(Guid id)
        //{
        //    string[] roleNames;
        //    using (BlobDbContext context = new BlobDbContext()) { 
        //        using (BlobUserStore userStore = new BlobUserStore(context)) {
        //            using (BlobUserManager userManager = new BlobUserManager(userStore))
        //            {
        //                roleNames = userManager.GetRolesAsync(id).Result.ToArray();
        //            }
        //        }
        //    }
        //    return roleNames;
        //}

        //protected override bool CheckAccessCore(OperationContext operationContext)
        //{
        //    string action = operationContext.IncomingMessageHeaders.Action;
        //    DispatchOperation operation = operationContext.EndpointDispatcher.DispatchRuntime.Operations.FirstOrDefault(o => o.Action == action);
        //    // Insert your own error-handling here if (operation == null)
        //    Type hostType = operationContext.Host.Description.ServiceType;
        //    MethodInfo method = hostType.GetMethod(operation.Name);
        //    return base.CheckAccessCore(operationContext);
        //}

        //protected override bool CheckAccessCore(OperationContext operationContext)
        //{
        //    _log.Debug("CheckAccessCore");

        //    IIdentity identity = operationContext.ServiceSecurityContext.PrimaryIdentity;

        //    Guid g;
        //    if (Guid.TryParse(identity.Name, out g))
        //    {
        //        // assume this is a device and not a user
        //        _log.Debug("Found a device.  add the device role and return");
        //        operationContext.ServiceSecurityContext.AuthorizationContext
        //                     .Properties["Principal"] = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, new[] { "Device" });

        //        return true;
        //    }

        //    using (BlobDbContext context = new BlobDbContext())
        //    {
        //        using (BlobUserStore userStore = new BlobUserStore(context))
        //        {
        //            using (BlobUserManager userManager = new BlobUserManager(userStore))
        //            {
        //                User user = userManager.FindByNameAsync2(identity.Name).Result;
        //                _log.Debug("Got user: " + user);

        //                if (user == null)
        //                {
        //                    string msg = String.Format("Unknown Username {0} .", identity.Name);
        //                    Trace.TraceWarning(msg);
        //                    _log.Debug(msg);
        //                    throw new FaultException(msg);
        //                }

        //                //Assign roles to the Principal property for runtime to match with PrincipalPermissionAttributes decorated on the service operation.
        //                string[] roleNames = userManager.GetRolesAsync(user.Id).Result.ToArray(); //users without any role assigned should then call operations not decorated by PrincipalPermissionAttributes
        //                operationContext.ServiceSecurityContext.AuthorizationContext
        //                    .Properties["Principal"] = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, roleNames);
        //                return true;
        //            }
        //        }

        //    }
        //}

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