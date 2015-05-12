using System;
using System.Linq;
using System.Security.Claims;

namespace Blob.Security.Authentication
{
    class ClaimTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            incomingPrincipal.Identities.First().AddClaim(new Claim("touched", DateTime.Now.ToString()));
            return incomingPrincipal;
        }
    }
}
