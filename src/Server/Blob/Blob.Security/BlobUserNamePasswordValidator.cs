using Blob.Data;
using log4net;
using Microsoft.AspNet.Identity;
using System;
using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Security
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
                    User user = AsyncHelper.RunSync(() => userManager.FindByNameAsync(userName));
                    _log.Debug("Got user: " + user);

                    if (user == null)
                    {
                        var msg = String.Format("Unknown Username {0} or incorrect password {1}", userName, password);
                        Trace.TraceWarning(msg);
                        throw new FaultException(msg); //the client actually will receive MessageSecurityException. But if I throw MessageSecurityException, the runtime will give FaultException to client without clear message.
                    }
                    else
                    {
                        Trace.TraceWarning(String.Format("Good Username {0} with correct password {1}", userName, password));
                    }
                }

            }

        }
    }
}
