using System.Web.Configuration;

namespace Before
{
    // http://www.dotnetperls.com/appsettings
    public static class SiteGlobalConfig
    {
        public static string AuthorizationService { get; set; }
        public static string AuthorizationServiceUsername { get; set; }
        public static string AuthorizationServicePassword { get; set; }

        static SiteGlobalConfig()
        {
            AuthorizationService = WebConfigurationManager.AppSettings["AuthorizationService"];
            AuthorizationServiceUsername = WebConfigurationManager.AppSettings["AuthorizationServiceUsername"];
            AuthorizationServicePassword = WebConfigurationManager.AppSettings["AuthorizationServicePassword"];
        }
    }
}
