using Blob.Core.Domain;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Blob.Security
{
    public class BlobUserValidator : UserValidator<User, Guid>
    {
        public BlobUserValidator(UserManager<User, Guid> manager)
            : base(manager)
        {
        }

        public new virtual Task<IdentityResult> ValidateAsync(User item)
        {
            return base.ValidateAsync(item);
        }
    }
}
