using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Blob.Security.Extensions
{
    public static class IdentityExtentions
    {
        public static string GetBlobId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue(SecurityConstants.BlobId);
            }
            return null;
        }

        //public static string GetName(this IIdentity identity)
        //{
        //    if (identity == null)
        //    {
        //        throw new ArgumentNullException("identity");
        //    }
        //    var ci = identity as ClaimsIdentity;
        //    if (ci != null)
        //    {
        //        return ci.FindFirstValue(SecurityConstants.BlobName);
        //    }
        //    return null;
        //}
        
        public static string GetCustomerId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue(SecurityConstants.CustomerId);
            }
            return null;
        }
    }
}
