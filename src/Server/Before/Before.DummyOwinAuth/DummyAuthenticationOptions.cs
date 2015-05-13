using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Before.DummyOwinAuth
{
    public class DummyAuthenticationOptions : AuthenticationOptions
    {
        public DummyAuthenticationOptions(string userName, string userId)
            : base(Constants.DefaultAuthenticationType)
        {
            Description.Caption = Constants.DefaultAuthenticationType;
            CallbackPath = new PathString("/signin-dummy");
            AuthenticationMode = AuthenticationMode.Passive;
            UserName = userName;
            UserId = userId;
        }

        public PathString CallbackPath { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string SignInAsAuthenticationType { get; set; }

        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }
    }
}
