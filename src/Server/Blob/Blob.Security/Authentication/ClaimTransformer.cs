using System;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using Blob.Contracts;
using Blob.Core;
using Blob.Core.Identity;
using Blob.Core.Identity.Store;
using Blob.Core.Models;
using Blob.Security.Extensions;
using Blob.Security.Identity;
using log4net;
using Microsoft.AspNet.Identity;

namespace Blob.Security.Authentication
{
    class ClaimTransformer : ClaimsAuthenticationManager
    {
        private readonly ILog _log;

        public ClaimTransformer()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing ClaimTransformer");
        }

        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            _log.Debug("Authenticate");
            incomingPrincipal.Identities.First().AddClaim(new Claim("touchedat", DateTime.Now.ToString()));
            incomingPrincipal.Identities.First().AddClaim(new Claim("touched", resourceName)); // this is the .svc url




            using (BlobDbContext context = new BlobDbContext())
            {
                var userName = incomingPrincipal.Identity.GetUserName();

                Guid g;
                if (Guid.TryParse(userName, out g))
                {
                    // assume this is a device and not a user
                    Device device = context.Devices.Find(g);
                    if (device != null && device.Enabled)
                    {
                        incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobIdClaimType, device.Id.ToString()));
                        incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobNameClaimType, device.DeviceName.ToString()));
                        incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobPrincipalTypeClaimType, SecurityConstants.DeviceTypeClaimType));
                        incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.CustomerIdClaimType, device.CustomerId.ToString()));
                    }
                }
                else
                {
                    using (BlobUserManager userManager = new BlobUserManager(new BlobUserStore(context)))
                    {
                        var user = userManager.FindByNameAsync2(userName).Result;
                        //todo implement the enabled checks
                        if (user != null)// && user.Enabled)
                        {
                            incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobIdClaimType, user.Id.ToString()));
                            incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobNameClaimType, user.UserName.ToString()));
                            incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobPrincipalTypeClaimType, SecurityConstants.UserTypeClaimType));
                            incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.CustomerIdClaimType, user.CustomerId.ToString()));
                        }
                    }
                }
            }
            return incomingPrincipal;
        }
    }
}
