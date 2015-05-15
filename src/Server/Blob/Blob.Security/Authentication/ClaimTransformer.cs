using System;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Data.Identity;
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
                        incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobId, device.Id.ToString()));
                        incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobName, device.DeviceName.ToString()));
                        incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobPrincipalType, SecurityConstants.DeviceType));
                        incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.CustomerId, device.CustomerId.ToString()));
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
                            incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobId, user.Id.ToString()));
                            incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobName, user.UserName.ToString()));
                            incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.BlobPrincipalType, SecurityConstants.UserType));
                            incomingPrincipal.Identities.First().AddClaim(new Claim(SecurityConstants.CustomerId, user.CustomerId.ToString()));
                        }
                    }
                }
            }
            return incomingPrincipal;
        }
    }
}
