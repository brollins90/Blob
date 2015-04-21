using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;

namespace Blob.Security
{
    public class BlobPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return (hashedPassword.Equals(providedPassword))
                       ? PasswordVerificationResult.Success
                       : PasswordVerificationResult.Failed;
        }
    }
}
