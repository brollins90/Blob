using System;
using System.Security.Claims;
using System.Web.Helpers;
using Blob.Contracts.ServiceContracts;
using Blob.Proxies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SimpleInjector;

namespace Before
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app, Container container)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            app.UseBeforeAuthorization(new BeforeAuthorizationClient(SiteGlobalConfig.AuthorizationService, SiteGlobalConfig.AuthorizationServiceUsername, SiteGlobalConfig.AuthorizationServicePassword));
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //ExpireTimeSpan = TimeSpan.FromMinutes(20),
                LoginPath = new PathString("/account/login"),
                //CookieSecure = CookieSecureOption.Always,
                //Provider = new CookieAuthenticationProvider
                //{
                //    OnResponseSignIn = async context =>
                //    {
                //        //This is the last chance before the ClaimsIdentity get serialized into a cookie. You can modify the ClaimsIdentity here and create the mapping here. This event is invoked one time on sign in. 
                //    },
                //    OnValidateIdentity = async context =>
                //    {
                //        //This method gets invoked for every request after the cookie is converted into a ClaimsIdentity. Here you can look up your claims from the mapping table. 
                //    }

                //    //OnValidateIdentity = context =>
                //    //{
                //    //    context.Ticket.Identity.AddClaim(new System.Security.Claims.Claim("foo", "var"));
                //    //    return Task.FromResult<object>(null);
                //    //}

                //    //// Enables the application to validate the security stamp when the user logs in.
                //    //// This is a security feature which is used when you change a password or add an external login to your account.  
                //    //OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                //    //    validateInterval: TimeSpan.FromMinutes(30),
                //    //    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                //}
            });

            // http://stackoverflow.com/questions/19192428/server-side-claims-caching-with-owin-authentication
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //                            {
            //                                Provider = new CookieAuthenticationProvider()
            //                                           {
            //                                               OnValidateIdentity = async context =>
            //                                               {
            //                                                   var userId = context.Identity.GetUserId(); //Just a simple extension method to get the ID using identity.FindFirst(x => x.Type == ClaimTypes.NameIdentifier) and account for possible NULLs
            //                                                   if (userId == null) return;
            //                                                   var cacheKey = "MyApplication_Claim_Roles_" + userId.ToString();
            //                                                   var cachedClaims = System.Web.HttpContext.Current.Cache[cacheKey] as IEnumerable<Claim>;
            //                                                   if (cachedClaims == null)
            //                                                   {
            //                                                       var securityService = DependencyResolver.Current.GetService<ISecurityService>(); //My own service to get the user's roles from the database
            //                                                       cachedClaims = securityService.GetRoles(context.Identity.Name).Select(role => new Claim(ClaimTypes.Role, role.RoleName));
            //                                                       System.Web.HttpContext.Current.Cache[cacheKey] = cachedClaims;
            //                                                   }
            //                                                   context.Identity.AddClaims(cachedClaims);
            //                                               }
            //                                           }
            //                            });

            ////app.CreatePerOwinContext(() => container.GetInstance<ApplicationDBContext>());
            //app.CreatePerOwinContext(() => container.GetInstance<IUserManagerService>());

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }

        //private void OnValidateIdentity()
        //{
            
        //}
    }

    //public static class UPCache
    //{
    //    public static void OnValidateIdentity()
    //    {
            
    //    }
    //}
}
