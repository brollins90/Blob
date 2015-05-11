using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using Blob.Data;
using Blob.Data.Identity;
using log4net;

namespace Blob.Security.Authentication
{
    public class BlobUserNamePasswordValidator : UserNamePasswordValidator
    {
        private readonly ILog _log;

        public BlobUserNamePasswordValidator()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobUserNamePasswordValidator");
        }

        public override void Validate(string userName, string password)
        {
            _log.Debug("Validate");

            using (BlobDbContext context = new BlobDbContext())
            {
                Guid g;
                string msg;
                if (Guid.TryParse(userName, out g))
                {
                    // assume this is a device and not a user
                    if (context.Devices.Find(g) != null) return;

                    msg = String.Format("Username {0} is a guid, but not a valid device", userName);
                    _log.Debug(msg);
                    throw new FaultException(msg);
                }

                using (BlobUserManager userManager = new BlobUserManager(new BlobUserStore(context)))
                {
                    if (!userManager.CheckUserNamePasswordAsync(userName, password).Result)
                    {
                        msg = String.Format("Validation failed({0},{1})", userName, password);
                        _log.Debug(msg);
                        throw new FaultException(msg);
                    }
                    _log.Debug("Validated");
                }
            }
        }
    }
}
