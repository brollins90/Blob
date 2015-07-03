// https://devzone.channeladam.com/articles/2014/09/how-to-easily-call-wcf-service-properly/

namespace Blob.Proxies
{
    using System;
    using System.ServiceModel;

    public class BaseDuplexClient<TChannel> : DuplexClientBase<TChannel>
        where TChannel : class
    {
        public Action<Exception> ClientErrorHandler = null;

        public BaseDuplexClient(string endpointName, string username, string password, InstanceContext callbackInstance)
            : base(callbackInstance, endpointName)
        {
            ConfigureCredentials(username, password);
        }

        protected void ConfigureCredentials(string username, string password)
        {
            ClientCredentials.UserName.UserName = username;
            ClientCredentials.UserName.Password = password;
            //ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
        }

        protected void HandleError(Exception ex)
        {
            if (ClientErrorHandler != null)
                ClientErrorHandler(ex);
            else
                throw new Exception("Server exception.", ex);
        }
    }
}