using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Data.Identity;
using log4net;

namespace Blob.Security.Sam
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
            _log.Debug(string.Format("Validate ({0}, {1})", userName, password));

            using (BlobDbContext context = new BlobDbContext())
            {
                Guid g;
                string msg;
                if (Guid.TryParse(userName, out g))
                {
                    // assume this is a device and not a user
                    Device device = context.Devices.Find(g);
                    if (device == null)
                    {
                        msg = String.Format("Username {0} is a guid, but not a valid device", userName);
                        _log.Debug(msg);
                        throw new FaultException(msg);
                    }
                    return;
                }

                using (BlobUserManager userManager = new BlobUserManager(new BlobUserStore(context)))
                {
                    if (!userManager.CheckUserNamePasswordAsync(userName, password).Result)
                    {
                        msg = String.Format("BlobUserNamePasswordValidator failed({0},{1})", userName, password);
                        _log.Debug(msg);
                        throw new FaultException(msg); //the client actually will receive MessageSecurityException. But if I throw MessageSecurityException, the runtime will give FaultException to client without clear message.
                    }
                }
            }
        }
    }
}