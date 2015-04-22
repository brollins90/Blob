using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Before.Startup))]
namespace Before
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SSLValidator.OverrideValidation();
            ConfigureAuth(app);
        }
    }

    public static class SSLValidator
    {
        private static RemoteCertificateValidationCallback _orgCallback;


        private static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        public static void OverrideValidation()
        {
            _orgCallback = ServicePointManager.ServerCertificateValidationCallback;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnValidateCertificate);
            ServicePointManager.Expect100Continue = true;
        }


        public static void RestoreValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback = _orgCallback;
        }
    }
}
