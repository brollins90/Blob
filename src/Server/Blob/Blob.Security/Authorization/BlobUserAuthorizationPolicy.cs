using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;

namespace Blob.Security.Authorization
{
    public class BlobUserAuthorizationPolicy : IAuthorizationPolicy
    {
        public string Id { get; private set; }
        public ClaimSet Issuer { get; private set; }

        public BlobUserAuthorizationPolicy()
        {
            Id = Guid.NewGuid().ToString();
            Issuer = ClaimSet.System;
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            bool bRet = false;
            CustomAuthState customstate = null;

            if (state == null)
            {
                customstate = new CustomAuthState();
                state = customstate;
            }
            else
                customstate = (CustomAuthState)state;

            Console.WriteLine("In Evaluate");
            if (!customstate.ClaimsAdded)
            {
                IList<Claim> claims = new List<Claim>();

                foreach (ClaimSet claimSet in evaluationContext.ClaimSets)
                {
                    foreach (Claim claim in claimSet.FindClaims(ClaimTypes.Name, Rights.PossessProperty))
                    {
                        foreach (string s in GetAllowedOpList(claim.Resource.ToString()))
                        {
                            claims.Add(new Claim("http://example.com/claims/allowedoperation", s, Rights.PossessProperty));
                            Console.WriteLine("Claim added {0}", s);
                        }
                    }
                }

                evaluationContext.AddClaimSet(this, new DefaultClaimSet(this.Issuer, claims));
                customstate.ClaimsAdded = true;
                bRet = true;
            }
            else
            {
                bRet = true;
            }
            return bRet;
        }

        // This method returns a collection of action strings that indicate the  
        // operations that the specified username is allowed to call. 
        private IEnumerable<string> GetAllowedOpList(string username)
        {
            IList<string> ret = new List<string>();

            if (username == "test1")
            {
                ret.Add("http://Microsoft.ServiceModel.Samples/ICalculator/Add");
                ret.Add("http://Microsoft.ServiceModel.Samples/ICalculator/Multiply");
                ret.Add("http://Microsoft.ServiceModel.Samples/ICalculator/Subtract");
            }
            else if (username == "test2")
            {
                ret.Add("http://Microsoft.ServiceModel.Samples/ICalculator/Add");
                ret.Add("http://Microsoft.ServiceModel.Samples/ICalculator/Subtract");
            }
            return ret;
        }

        // internal class for state 
        class CustomAuthState
        {

            public CustomAuthState()
            {
                ClaimsAdded = false;
            }

            public bool ClaimsAdded { get; set; }
        }
    }
}

//    public class ClaimsAuthorizationPolicy : IAuthorizationPolicy
//    {
//            //public const string IssuerName = "http://www.thatindigogirl.com/samples/2006/06/issuer";

//        public BlobAuthorizationPolicy()
//        {
//            Id = Guid.NewGuid().ToString();
//            Issuer = ClaimSet.System;

//            //Claim c = Claim.CreateNameClaim(ClaimsAuthorizationPolicy.IssuerName);
//            //Claim[] claims = new Claim[1];
//            //claims[0] = c;
//            //DefaultClaimSet issuerClaimSet = new DefaultClaimSet(claims);
//        }

//        public string Id { get; private set; }
//        public ClaimSet Issuer { get; private set; }

//        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
//        {
//            if (evaluationContext.Properties.ContainsKey("Identities"))
//            {
//                List<IIdentity> identities = evaluationContext.Properties["Identities"] as List<IIdentity>;
//                IIdentity identity = identities[0];

//                ClaimSet claims = MapClaims(identity);

//                GenericPrincipal newPrincipal = new GenericPrincipal(identity, null);
//                evaluationContext.Properties["Principal"] = newPrincipal;

//                if (claims != null)
//                    evaluationContext.AddClaimSet(this, claims);

//                return true;
//            }
//            else
//                return false;
//        }

//        protected virtual ClaimSet MapClaims(IIdentity identity)
//        {
//            List<Claim> listClaims = new List<Claim>();

//            //if (Roles.IsUserInRole(identity.Name, "Guests"))
//            //{
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Read,
//            //ClaimsAuthorizationPolicy.Resources.Customers,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Read,
//            //ClaimsAuthorizationPolicy.Resources.Orders,
//            //Rights.PossessProperty));
//            //}
//            //else if (Roles.IsUserInRole(identity.Name, "Users"))
//            //{
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Read,
//            //ClaimsAuthorizationPolicy.Resources.Customers,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Read,
//            //ClaimsAuthorizationPolicy.Resources.Orders,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Create,
//            //ClaimsAuthorizationPolicy.Resources.Customers,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Create,
//            //ClaimsAuthorizationPolicy.Resources.Orders,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Update,
//            //ClaimsAuthorizationPolicy.Resources.Customers,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Update,
//            //ClaimsAuthorizationPolicy.Resources.Orders,
//            //Rights.PossessProperty));

//            //}
//            //else if (Roles.IsUserInRole(identity.Name, "Administrators"))
//            //{
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Read,
//            //ClaimsAuthorizationPolicy.Resources.Customers,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Read,
//            //ClaimsAuthorizationPolicy.Resources.Orders,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Create,
//            //ClaimsAuthorizationPolicy.Resources.Customers,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Create,
//            //ClaimsAuthorizationPolicy.Resources.Orders,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Update,
//            //ClaimsAuthorizationPolicy.Resources.Customers,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Update,
//            //ClaimsAuthorizationPolicy.Resources.Orders,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Delete,
//            //ClaimsAuthorizationPolicy.Resources.Customers,
//            //Rights.PossessProperty));
//            //    listClaims.Add(new Claim(ClaimsAuthorizationPolicy.ClaimTypes.Delete,
//            //ClaimsAuthorizationPolicy.Resources.Orders,
//            //Rights.PossessProperty));
//            //}


//            return new DefaultClaimSet(ClaimSet.System, listClaims);
//        }
//    }
//}
