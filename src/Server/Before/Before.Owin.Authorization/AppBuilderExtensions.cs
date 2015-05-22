using Before.Owin.Authorization;
using Blob.Contracts.ServiceContracts;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseBeforeAuthorization(this IAppBuilder app, IAuthorizationManagerService authorizationManager)
        {
            var options = new BeforeAuthorizationMiddlewareOptions
            {
                Manager = authorizationManager
            };

            app.UseBeforeAuthorization(options);
            return app;
        }

        public static IAppBuilder UseBeforeAuthorization(this IAppBuilder app, BeforeAuthorizationMiddlewareOptions options)
        {
            app.Use(typeof(BeforeAuthorizationMiddleware), options);
            return app;
        }
        //public static IAppBuilder UseBeforeAuthorization(this IAppBuilder app)
        //{
        //    BeforeAuthorizationMiddlewareOptions options;
        //    options = new BeforeAuthorizationMiddlewareOptions
        //    {
        //        Manager = new BeforeAuthorizationClient("AuthorizationService", "customerUser1", "password")
        //    };

        //    app.Use(typeof(BeforeAuthorizationManagerMiddleware), options);
        //    return app;
        //}
    }
}
