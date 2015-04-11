using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace Blob.Services.Authentication
{
    public class BlobUserNamePasswordValidator : UserNamePasswordValidator 
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new SecurityTokenException("The username and password are required");
            }
            // todo: really check...
            // if not authenticated, throw an exception
        }
    }
}
