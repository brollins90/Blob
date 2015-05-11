//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.ServiceModel;
//using System.Text;
//using System.Threading.Tasks;
//using Blob.Data;
//using Blob.Data.Identity;
//using Blob.Security.Authorization;
//using log4net;
//using Microsoft.AspNet.Identity;

//namespace Blob.Security.Authentication
//{
//    internal class BlobClaimsAuthenticationManager : ClaimsAuthenticationManager
//    {
//        private ILog _log;

//        public BlobClaimsAuthenticationManager()
//        {
//            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
//            _log.Debug("Constructing BlobClaimsAuthorizationManager");
//        }

//        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
//        {
//            //add the tenantId claim into the token so we can use it throughout web app (and only lookup once!)
//            if (incomingPrincipal.Identity.IsAuthenticated)
//            {
//                var identity = ((ClaimsIdentity)incomingPrincipal.Identity);
//                var tenantId = identity.Claims.FirstOrDefault(c => c.Type == ClaimConstants.CustomerId);

//                if (tenantId == null || String.IsNullOrWhiteSpace(tenantId.Value))
//                {
//                    var provider = identity.Claims.SingleOrDefault(c => c.Type == ClaimConstants.IdentityProvider);
//                    var nameIdentifier = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
//                    var customerId = Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f");//= this.tenantRepository.GetOrAddTenantId(provider.Value, nameIdentifier.Value);

//                    identity.AddClaim(new Claim(ClaimConstants.CustomerId, customerId.ToString()));
//                }
//            }
//            return base.Authenticate(resourceName, incomingPrincipal);
//        }

//        //return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));

//        //    ClaimsPrincipal cp = incomingPrincipal;
//        //    List<IIdentity> identities = evaluationContext.Properties["Identities"] as List<IIdentity>;
//        //    ClaimsIdentityCollection claimsIds = new ClaimsIdentityCollection();
//        //    if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated == true)
//        //    {
//        //        ClaimsIdentity newClaimsId = new ClaimsIdentity(
//        //            "CustomClaimsAuthenticationManager", ClaimTypes.Name,
//        //            "urn:ClaimsAwareWebSite/2010/01/claims/permission");
//        //        ClaimsIdentity claimsId =
//        //            incomingPrincipal.Identity as ClaimsIdentity;
//        //        foreach (Claim c in claimsId.Claims)
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       c.ClaimType, c.Value, c.ValueType,
//        //                                       "CustomClaimsAuthenticationManager", c.Issuer));
//        //        if (incomingPrincipal.IsInRole("Administrators"))
//        //        {
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       "urn:ClaimsAwareWebSite/2010/01/claims/permission",
//        //                                       "Create"));
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       "urn:ClaimsAwareWebSite/2010/01/claims/permission",
//        //                                       "Read"));
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       "urn:ClaimsAwareWebSite/2010/01/claims/permission",
//        //                                       "Update"));
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       "urn:ClaimsAwareWebSite/2010/01/claims/permission",
//        //                                       "Delete"));
//        //        }
//        //        else if (incomingPrincipal.IsInRole("Users"))
//        //        {
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       "urn:ClaimsAwareWebSite/2010/01/claims/permission",
//        //                                       "Create"));
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       "urn:ClaimsAwareWebSite/2010/01/claims/permission",
//        //                                       "Read"));
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       "urn:ClaimsAwareWebSite/2010/01/claims/permission",
//        //                                       "Update"));
//        //        }
//        //        else
//        //        {
//        //            newClaimsId.Claims.Add(new Claim(
//        //                                       "urn:ClaimsAwareWebSite/2010/01/claims/permission",
//        //                                       "Read"));
//        //        }
//        //        claimsIds.Add(newClaimsId);
//        //        cp = new ClaimsPrincipal(claimsIds);
//        //    }
//        //    return cp;
//        //}
//    }
//}
