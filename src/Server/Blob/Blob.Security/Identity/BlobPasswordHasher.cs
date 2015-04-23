using Microsoft.AspNet.Identity;

namespace Blob.Security.Identity
{
    public class BlobPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null || providedPassword == null)
                return PasswordVerificationResult.Success;

            return (hashedPassword.Equals(providedPassword))
                       ? PasswordVerificationResult.Success
                       : PasswordVerificationResult.Failed;
        }
    }
}
