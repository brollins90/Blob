using Blob.Core.Domain;
using Microsoft.AspNet.Identity;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Blob.Contracts.Security;
using log4net;

namespace Blob.Security
{
    public class BlobUserValidator : IIdentityValidator<User>
// UserValidator<User, Guid>
    {
        private readonly ILog _log;

        public BlobUserValidator(UserManager<User, Guid> manager)
            //: base(manager)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobUserManager");
        }

        public virtual Task<IdentityResult> ValidateAsync(User item)
        {
            _log.Debug("ValidateAsync");

            return Task.FromResult(IdentityResult.Success);
            // return true.;//return base.ValidateAsync(item);
        }
    }
}
