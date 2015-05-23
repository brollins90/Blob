using System.Collections.Generic;
using System.Web.Configuration;

namespace Before
{
    // http://www.dotnetperls.com/appsettings
    public static class SiteGlobalConfig
    {
        public static string AuthorizationService { get; set; }
        public static string AuthorizationServiceUsername { get; set; }
        public static string AuthorizationServicePassword { get; set; }
        public static Dictionary<string, string> UpDictionary { get; set; }
        
        static SiteGlobalConfig()
        {
	    // Cache all these values in static properties.
            AuthorizationService = WebConfigurationManager.AppSettings["AuthorizationService"];
            AuthorizationServiceUsername = WebConfigurationManager.AppSettings["AuthorizationServiceUsername"];
            AuthorizationServicePassword = WebConfigurationManager.AppSettings["AuthorizationServicePassword"];

            UpDictionary = new Dictionary<string, string>();
        }

    }
}