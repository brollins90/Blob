using System;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace Blob.Proxies
{
    public class BaseClient<TChannel> : ClientBase<TChannel>
        where TChannel : class
    {
        public Action<Exception> ClientErrorHandler = null;

        public BaseClient(string endpointName, string username, string password)
            : base(endpointName)
        {
            ConfigureCredentials(username, password);
        }

        public BaseClient(string endpointName, string username, string password, InstanceContext callbackInstance)
            : base(callbackInstance, endpointName)
        {
            ConfigureCredentials(username, password);
        }

        protected void ConfigureCredentials(string username, string password)
        {
            ClientCredentials.UserName.UserName = username;
            ClientCredentials.UserName.Password = password;
            ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
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
