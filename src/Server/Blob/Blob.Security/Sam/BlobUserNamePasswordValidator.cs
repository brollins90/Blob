using System;
using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.ServiceModel;
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
                using (BlobUserManager userManager = new BlobUserManager(new BlobUserStore(context)))
                {
                    _log.Debug(string.Format("Validating username: {0} with password {1}", userName, password));

                    string msg;

                    if (userManager.CheckUserNamePasswordAsync(userName, password).Result)
                    {
                        msg = string.Format("Valid User {0} with correct password {1}", userName, password);
                        Trace.TraceWarning(msg);
                        _log.Debug(msg);
                    }
                    else
                    {
                        msg = String.Format("Unknown Username {0} or incorrect password {1}", userName, password);
                        Trace.TraceWarning(msg);
                        _log.Debug(msg);
                        throw new FaultException(msg); //the client actually will receive MessageSecurityException. But if I throw MessageSecurityException, the runtime will give FaultException to client without clear message.
                    }
                }
            }
        }
    }
}
