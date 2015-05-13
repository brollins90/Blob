using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blob.Contracts.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

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



        #region Signin stuff
        public static string GetAuthenticationType(this IUserManagerService ums)
        {
            return DefaultAuthenticationTypes.ApplicationCookie; 
        }
        
        public static async Task SignInAsync(this IUserManagerService ums, UserDto user, bool isPersistent, bool rememberBrowser)
        {
            ClaimsIdentity userIdentity = await ums.CreateIdentityAsync(user, ums.GetAuthenticationType());
            // Clear any partial cookies from external or two factor partial sign ins
            //await SignOutAsync(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            //if (rememberBrowser)
            //{
            //    ClaimsIdentity rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id);
            //    await SignInAsync(new AuthenticationPropertiesDto { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            //}
            //else
            //{
            await ums.SignInAsync(new AuthenticationPropertiesDto { IsPersistent = isPersistent }, userIdentity);
            //}
        }

        /// <summary>
        /// Sign in the user in using the user name and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        public static async Task<SignInStatusDto> PasswordSignInAsync(this IUserManagerService ums, string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = await ums.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInStatusDto.Failure;
            }
            await ums.SignInAsync(user, isPersistent, false);
            return SignInStatusDto.Success;
            //if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            //{
            //    return SignInStatusDto.LockedOut;
            //}
            //if (await UserManager.CheckPasswordAsync(user, password).WithCurrentCulture())
            //{
            //    await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
            //    return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
            //}
            //if (shouldLockout)
            //{
            //    // If lockout is requested, increment access failed count which might lock out the user
            //    await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
            //    if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            //    {
            //        return SignInStatus.LockedOut;
            //    }
            //}
            //return SignInStatus.Failure;
        }

        #endregion
    }
}