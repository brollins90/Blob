using log4net;
using Microsoft.AspNet.Identity;

namespace Blob.Security.Identity
{
    public class BlobPasswordHasher : IPasswordHasher
    {
        private readonly ILog _log;

        public BlobPasswordHasher()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobPasswordHasher");
        }

        public string HashPassword(string password)
        {
            _log.Debug("HashPassword");
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            _log.Debug("VerifyHashedPassword");

            if (hashedPassword == null || providedPassword == null)
                return PasswordVerificationResult.Success;

            return (hashedPassword.Equals(providedPassword))
                       ? PasswordVerificationResult.Success
                       : PasswordVerificationResult.Failed;
        }
    }
}
