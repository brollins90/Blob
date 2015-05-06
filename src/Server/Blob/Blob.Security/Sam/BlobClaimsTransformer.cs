//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Security;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.ServiceModel;
//using System.Threading;
//using Blob.Core.Domain;
//using Blob.Data;
//using Blob.Data.Identity;
//using log4net;

//namespace Blob.Managers.Blob
//{
//    public class BlobClaimsTransformer : ClaimsAuthenticationManager
//    {
//        private readonly ILog _log;

//        public BlobClaimsTransformer()
//        {
//            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
//            _log.Debug("Constructing BlobClaimsTransformer");
//        }

//        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
//        {
//            _log.Debug(string.Format("Authenticate({0}, {1})", resourceName, ""));

//            //validate name claim
//            string nameClaimValue = incomingPrincipal.Identity.Name;

//            if (string.IsNullOrEmpty(nameClaimValue))
//            {
//                throw new SecurityException("A user with no name???");
//            }

//            return CreatePrincipal(nameClaimValue);

//            //return base.Authenticate(resourceName, incomingPrincipal);
//        }

//        private ClaimsPrincipal CreatePrincipal(string userName)
//        {
//            _log.Debug("CreatePrincipal");

//            ClaimsPrincipal newPrincipal;

//            using (BlobDbContext context = new BlobDbContext())
//            {
//                Guid g;
//                if (Guid.TryParse(userName, out g))
//                {
//                    // assume this is a device and not a user
//                    _log.Debug("Found a device.  add the device role and return");
//                    newPrincipal = new ClaimsPrincipal();
                    
//                    newPrincipal = new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, new[] {"Device"});
                    

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
//                        p.
                        
//                        operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] =
//                            //new GenericPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity, roleNames);
//                            new ClaimsPrincipal(operationContext.ServiceSecurityContext.PrimaryIdentity);

//                        return true;
//                    }
//                }

//            }
//            Device device = 

//            if (userName.IndexOf("andras", StringComparison.InvariantCultureIgnoreCase) > -1)
//            {
//                likesJavaToo = true;
//            }

//            List<Claim> claimsCollection = new List<Claim>()
//    {
//        new Claim(ClaimTypes.Name, userName)
//        , new Claim("http://www.mysite.com/likesjavatoo", likesJavaToo.ToString())
//    };

//            return new ClaimsPrincipal(new ClaimsIdentity(claimsCollection, "Custom"));
//        }
//    }
//}
