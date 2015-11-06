using System.Web.Configuration;

namespace Before
{
    // http://www.dotnetperls.com/appsettings
    public static class SiteGlobalConfig
    {
        public static string AuthorizationService { get; set; }
        public static string AuthorizationServiceUsername { get; set; }
        public static string AuthorizationServicePassword { get; set; }
        public static string Environment { get; set; }

        static SiteGlobalConfig()
        {
            AuthorizationService = WebConfigurationManager.AppSettings["AuthorizationService"];
            AuthorizationServiceUsername = WebConfigurationManager.AppSettings["AuthorizationServiceUsername"];
            AuthorizationServicePassword = WebConfigurationManager.AppSettings["AuthorizationServicePassword"];
            Environment = WebConfigurationManager.AppSettings["Environment"];
        }
    }

    // http://stackoverflow.com/a/19599965/148256
    //public static class AppSettings
    //{

    //    public static string ClientSecret
    //    {
    //        get
    //        {
    //            return Setting<string>("ClientSecret");
    //        }
    //    }

    //    private static T Setting<T>(string name)
    //    {
    //        string value = ConfigurationManager.AppSettings[name];

    //        if (value == null)
    //        {
    //            throw new Exception(String.Format("Could not find setting '{0}',", name));
    //        }

    //        return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
    //    }
    //}
}
