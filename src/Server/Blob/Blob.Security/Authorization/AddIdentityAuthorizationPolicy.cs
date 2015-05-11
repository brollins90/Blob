using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Claims;
//using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Claim = System.IdentityModel.Claims.Claim;

namespace Blob.Security.Authorization
{
    public class AddIdentityAuthorizationPolicy : IAuthorizationPolicy
    {
        private readonly ILog _log;

        public string Id { get; private set; }
        public ClaimSet Issuer { get; private set; }

        public AddIdentityAuthorizationPolicy()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing AddIdentityAuthorizationPolicy");

            Id = Guid.NewGuid().ToString();
            Issuer = ClaimSet.System;
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // get the authenticated client identity
            IIdentity client = GetClientIdentity(evaluationContext);
            
        //    IList<Claim> claims = new List<Claim>();
        //    // Iterate through the various claim sets in the AuthorizationContext.
        //    foreach (ClaimSet cs in evaluationContext.ClaimSets)//.ServiceSecurityContext.AuthorizationContext.ClaimSets)
        //    {
        //        foreach (var c in cs)
        //        {
        //            list.Add(c);
        //        }
        //    }

        //    public static ClaimsIdentity CreateClaimsIdentityFromClaimSet(System.IdentityModel.Claims.ClaimSet claimset, string authenticationType)
        //{
        //    if (claimset == null)
        //    {
        //        throw new ArgumentNullException("claimSet");
        //    }
 
        //    string issuer = null;
        //    if (claimset.Issuer == null)
        //    {
        //        issuer = ClaimsIdentity.DefaultIssuer;
        //    }
        //    else
        //    {
        //        foreach (System.IdentityModel.Claims.Claim claim in claimset.Issuer.FindClaims(System.IdentityModel.Claims.ClaimTypes.Name, System.IdentityModel.Claims.Rights.Identity))
        //        {
        //            if ((claim != null) && (claim.Resource is string))
        //            {
        //                issuer = claim.Resource as string;
        //                break;
        //            }
        //        }
        //    }
 
        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(authenticationType);
 
        //    for (int i = 0; i < claimset.Count; ++i)
        //    {
        //        //
        //        // Only capture possesses property claims
        //        //
        //        if (String.Equals(claimset[i].Right, System.IdentityModel.Claims.Rights.PossessProperty, StringComparison.Ordinal))
        //        {
        //            claimsIdentity.AddClaim(CreateClaimFromWcfClaim(claimset[i], issuer));
        //        }
        //    }
 
        //    return claimsIdentity;
        //}

            //evaluationContext.Properties["Principal"] = new ClaimsIdentity(claims: list, authenticationType: client.AuthenticationType); 

            return true;
        }

        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new Exception("No Identity found");

            return identities[0];
        }
    }
}
