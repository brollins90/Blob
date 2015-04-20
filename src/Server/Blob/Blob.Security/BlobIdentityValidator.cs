using Blob.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace Blob.Security
{
    public class BlobIdentityValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            using (var context = new BlobDbContext())
            {
                using (var userManager = new BlobUserManager(new BlobUserStore(context)))
                {
                    var user = userManager.Find(userName, password);
                    if (user == null)
                    {
                        var msg = String.Format("Unknown Username {0} or incorrect password {1}", userName, password);
                        Trace.TraceWarning(msg);
                        //throw new FaultException(msg);//the client actually will receive MessageSecurityException. But if I throw MessageSecurityException, the runtime will give FaultException to client without clear message.
                    }
                    Trace.TraceWarning(String.Format("Good Username {0} with correct password {1}", userName, password));
                }

            }

        }
    }
}
