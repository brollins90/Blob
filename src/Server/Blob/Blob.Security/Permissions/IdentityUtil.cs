using System.Security.Claims;
using System.Security.Principal;
using Blob.Security.Authorization;

namespace Blob.Security.Permissions
{
    public static class IdentityUtil
    {
        public static bool HasClaimFor(this IIdentity identity, string resource, string operation)
        {
            ClaimsIdentity ci = identity as ClaimsIdentity;

            var all = ci.FindAll(c => c.Type.Equals(ClaimConstants.AllInOne));

            foreach (var claim in all)
            {
                if (claim.Value.Contains(resource) && claim.Value.Contains(operation))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
