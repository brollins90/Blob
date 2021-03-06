﻿using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using Blob.Core.Identity;
using Blob.Core.Identity.Store;
using Blob.Core.Models;
using log4net;

namespace Blob.Core.Authentication
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
            _log.Debug("Validating Username and password");

            //if (userName.ToLowerInvariant().Equals("beforeuser") && password.ToLowerInvariant().Equals("beforepassword"))
            //    return;

            using (BlobDbContext context = new BlobDbContext())
            {
                Guid g;
                string msg;
                if (Guid.TryParse(userName, out g))
                {
                    // assume this is a device and not a user
                    Device device = context.Devices.Find(g);
                    if (device != null && device.Enabled) 
                        return;

                    msg = String.Format("Username {0} is a guid, but not a valid device or the device is not enabled", userName);
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
