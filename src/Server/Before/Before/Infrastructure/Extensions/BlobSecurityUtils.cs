using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Before.Infrastructure.Extensions
{
    public static class BlobSecurityUtils
    {
        public static Task<bool> CheckAccessAsync(this HttpContextBase httpContext, AuthorizationContextDto authorizationContext)
        {
            return httpContext.GetOwinContext().CheckAccessAsync(authorizationContext);
        }

        private static async Task<bool> CheckAccessAsync(this IOwinContext context, AuthorizationContextDto authorizationContext)
        {
            return await context.GetAuthorizationManager().CheckAccessAsync(authorizationContext).ConfigureAwait(false);
        }

        private static IAuthorizationManagerService GetAuthorizationManager(this IOwinContext context)
        {
            var am = context.Get<IAuthorizationManagerService>("BeforeAuthorizationClient");

            if (am == null)
            {
                throw new InvalidOperationException("No AuthorizationManager set.");
            }

            return am;
        }

        public static IdentityResultDto ToDto(this IdentityResult result)
        {
            return new IdentityResultDto { Succeeded = result.Succeeded, Errors = result.Errors };
        }

        public static IdentityResult ToResult(this IdentityResultDto dto)
        {
            return (dto.Succeeded)
                ? IdentityResult.Success
                : IdentityResult.Failed(dto.Errors.ToArray());
        }

        public static SignInStatusDto ToDto(this SignInStatus result)
        {
            return EnumUtils.ParseEnum<SignInStatusDto>(result.ToString());
        }

        public static SignInStatus ToResult(this SignInStatusDto dto)
        {
            return EnumUtils.ParseEnum<SignInStatus>(dto.ToString());
        }

        public static Guid GetCustomerId(this IIdentity identity)
        {
            return Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f");
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