using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Before.Infrastructure.Identity;
using Blob.Contracts.Security;
using Blob.Proxies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Before.Models
{
    


    

    

    public static class Helper
    {
        public static string GetCustomerId(this IIdentity identity)
        {
            return Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f").ToString();
            //if (identity == null)
            //{
            //    throw new ArgumentNullException("identity");
            //}
            //var ci = identity as ClaimsIdentity;
            //if (ci != null)
            //{
            //    return ci.FindFirstValue("customerid");
            //    //return ci.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
            //}
            //return null;
        }
    }
}