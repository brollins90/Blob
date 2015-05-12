using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Xml;
using log4net;

namespace Blob.Security.Authorization
{
    public class BlobClaimsAuthorizationManager : ClaimsAuthorizationManager
    {
        private readonly ILog _log;

        public BlobClaimsAuthorizationManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobClaimsAuthorizationManager");
        }

        public override bool CheckAccess(AuthorizationContext context)
        {
            _log.Debug("CheckAccess");
            ClaimsIdentity claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            // If there is not a claim of Name, then throw.  You need a name...
            if (!claimsIdentity.Claims.Any(x => x.Type.Equals(ClaimTypes.Name)))
                throw new SecurityException("Access is denied.");

            IEnumerable<Claim> resourceClaims = context.Resource.Where(x => x.Type == ClaimTypes.Name);

            if (resourceClaims.Any())
            {
                foreach (Claim c in resourceClaims)
                {
                    _log.Debug(string.Format("R:(T:{0}, V:{1})", c.Type, c.Value));

                    if (c.Value.Contains("AdminOnly") && !context.Principal.IsInRole("Administrators"))
                        throw new SecurityException("Access is denied.");
                }
            }
            return true;
        }

        public override void LoadCustomConfiguration(XmlNodeList nodelist)
        {
            _log.Debug("LoadCustomConfiguration but i am not using anything here.");
        }
    }
}