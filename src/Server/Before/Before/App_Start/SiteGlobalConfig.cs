using System.Web.Configuration;

namespace Before
{
    // http://www.dotnetperls.com/appsettings
    public static class SiteGlobalConfig
    {
        static public string AuthorizationService { get; set; }
        static public string AuthorizationServiceUsername { get; set; }
        static public string AuthorizationServicePassword { get; set; }
        static SiteGlobalConfig()
        {
	    // Cache all these values in static properties.
            AuthorizationService = WebConfigurationManager.AppSettings["AuthorizationService"];
            AuthorizationServiceUsername = WebConfigurationManager.AppSettings["AuthorizationServiceUsername"];
            AuthorizationServicePassword = WebConfigurationManager.AppSettings["AuthorizationServicePassword"];
        }
    }
}