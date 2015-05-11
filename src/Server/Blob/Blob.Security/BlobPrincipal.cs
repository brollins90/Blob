using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Blob.Security
{
    public static class ClaimsPrincipalExtensions

{

    public static bool IsOperator(this ClaimsPrincipal principal)

    {

        return principal.HasClaim(ClaimTypes.Role, "Operator");

    }

} 

    //public class BlobClaimsPrincipal : ClaimsPrincipal
    //{
    //    public static new BlobClaimsPrincipal Current
    //    {

    //        get
    //        {

    //            return new BlobClaimsPrincipal(ClaimsPrincipal.Current);

    //        }

    //    } 
    //    public BlobClaimsPrincipal(ClaimsPrincipal principal) : base(principal) { }

    //    public bool IsOperator
    //    {
    //        get { return HasClaim(ClaimTypes.Role, "Operator"); }
    //    }
    //}
}

//    // https://msdn.microsoft.com/en-us/magazine/cc948343.aspx
//    class BlobPrincipal : IPrincipal
//    {
//        IIdentity _identity;
//        string[] _roles;
//        Cache _cache = HttpRuntime.Cache;

//        public BlobPrincipal(IIdentity identity)
//        {
//            _identity = identity;
//        }

//        // helper method for easy access (without casting)
//        public static BlobPrincipal Current
//        {
//            get
//            {
//                return Thread.CurrentPrincipal as BlobPrincipal;
//            }
//        }

//        public IIdentity Identity
//        {
//            get { return _identity; }
//        }

//        // return all roles (custom property)
//        public string[] Roles
//        {
//            get
//            {
//                EnsureRoles();
//                return _roles;
//            }
//        }

//        // IPrincipal role check
//        public bool IsInRole(string role)
//        {
//            EnsureRoles();

//            return _roles.Contains(role);
//        }

//        // cache roles for subsequent requests
//        protected virtual void EnsureRoles()
//        {
//            // caching logic omitted – see the sample download
//        }
//    }
//}
