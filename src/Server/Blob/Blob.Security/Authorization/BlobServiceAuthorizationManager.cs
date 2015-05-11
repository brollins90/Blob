

using System;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml.XPath;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Data.Identity;
using log4net;

namespace Blob.Security.Authorization
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
            // i think that in the magic line beneath me, all the auth policies get evaluated...
            IIdentity identity = operationContext.ServiceSecurityContext.PrimaryIdentity;

            _log.Debug(ShowAuthorizationContext());
            string action = operationContext.IncomingMessageHeaders.Action;

            //// Iterate through the various claim sets in the AuthorizationContext.
            //foreach (ClaimSet cs in operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets)
            //{
            //    // Examine only those claim sets issued by System.
            //    if (cs.Issuer == ClaimSet.System)
            //    {
            //        // Iterate through claims of type "http://www.contoso.com/claims/allowedoperation".
            //        foreach (Claim c in cs)// cs.FindClaims(ClaimPermission.ActionType, Rights.PossessProperty))
            //        {
            //            // If the Claim resource matches the action URI then return true to allow access.
            //            if (action == c.Resource.ToString())
            //                return true;
            //        }
            //    }
            //}

            Guid g;
            if (Guid.TryParse(identity.Name, out g))
            {
                // assume this is a device and not a user
                _log.Debug("Found a device.  add the device role and return");
                operationContext.ServiceSecurityContext.AuthorizationContext
                             .Properties["Principal"] = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, new[] { "Device" });

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


            //_log.Debug("CheckAccessCore");
            //var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            ////Make sure they are authenticated
            //if (!prinicpal.Identity.IsAuthenticated)
            //{
            //    _log.Debug("CheckAccessCore-failed, not authenticated");
            //    return false;
            //}

            //_log.Debug("CheckAccessCore-passed, cause i didnt check");
            //return true;



            //// Extract the action URI from the OperationContext. Match this against the claims
            //// in the AuthorizationContext.
            //string action = operationContext.RequestContext.RequestMessage.Headers.Action;

            // Iterate through the various claim sets in the AuthorizationContext.
            //foreach (ClaimSet cs in operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets)
            //{
            //    // Examine only those claim sets issued by System.
            //    if (cs.Issuer == ClaimSet.System)
            //    {
            //        // Iterate through claims of type "http://www.contoso.com/claims/allowedoperation".
            //        foreach (Claim c in cs.FindClaims(ClaimPermission.ActionType, Rights.PossessProperty))
            //        {
            //            // If the Claim resource matches the action URI then return true to allow access.
            //            if (action == c.Resource.ToString())
            //                return true;
            //        }
            //    }
            //}

            //// If this point is reached, return false to deny access.
            //return false;
        }
                public string ShowAuthorizationContext()
                {
                    StringBuilder sb = new StringBuilder();
                    AuthorizationContext context = ServiceSecurityContext.Current.AuthorizationContext;

                    foreach (ClaimSet set in context.ClaimSets) 
                    {
                        sb.Append("\nIssuer:\n");
                        sb.Append(set.Issuer.GetType().Name);
                        foreach (Claim claim in set.Issuer) 
                        {
                            ShowClaim(claim);
                        }

                        sb.Append("\nIssued:\n");
                        sb.Append(set.GetType().Name);
                        foreach (Claim claim in set) 
                        {
                            ShowClaim(claim);
                        }
                    }
                    return sb.ToString();
                }

        private string ShowClaim(Claim claim) 
        {
            return string.Format("{0}\n{1}\n{2}\n",
                claim.ClaimType,
                claim.Resource,
                claim.Right);
        }

        //        //public override bool CheckAccess(AuthorizationContext context)
        //        //{
        //        //    string resourceStr = context.Resource.First().Value;
        //        //    string actionStr = context.Action.First().Value;


        //        //    //int resourceId;
        //        //    //Operations operationId;
        //        //    //try { resourceId = Int32.Parse(resourceStr); }
        //        //    //catch { throw new Exception("Invalid resource. Must be a string representation of an integer value."); }
        //        //    //try { operationId = (Operations)Enum.Parse(typeof(Operations), actionStr); }
        //        //    //catch { throw new Exception("Invalid action/operation. Must be a string representation of an integer value."); }

        //        //    //Get the current claims principal
        //        //    var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;
        //        //    //Make sure they are authenticated
        //        //    if (!prinicpal.Identity.IsAuthenticated)
        //        //        return false;
        //        //    //Get the roles from the claims
        //        //    var roles = prinicpal.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToArray();
        //        //    //Check if they are authorized
        //        //    //return ResourceService.Authorize(resourceId, operationId, roles);

        //        //}

        //        //private string[] getRoles(Guid id)
        //        //{
        //        //    string[] roleNames;
        //        //    using (BlobDbContext context = new BlobDbContext()) { 
        //        //        using (BlobUserStore userStore = new BlobUserStore(context)) {
        //        //            using (BlobUserManager userManager = new BlobUserManager(userStore))
        //        //            {
        //        //                roleNames = userManager.GetRolesAsync(id).Result.ToArray();
        //        //            }
        //        //        }
        //        //    }
        //        //    return roleNames;
        //        //}
    }
}
