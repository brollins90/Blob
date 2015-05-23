using System;
using System.Threading.Tasks;
using System.Web;
using Before.Owin.Authorization;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Microsoft.Owin;

namespace Before.Infrastructure.Extensions
{
    public static class BeforeAuthorizationMiddlewareUtils
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
            var am = context.Get<IAuthorizationManagerService>(BeforeAuthorizationMiddleware.KEY);

            if (am == null)
            {
                throw new InvalidOperationException("No AuthorizationManager set.");
            }
            return am;
        }
    }
}
