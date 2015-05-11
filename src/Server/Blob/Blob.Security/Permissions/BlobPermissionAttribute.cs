using System;
using System.Security;
using System.Security.Permissions;
using log4net;

namespace Blob.Security.Permissions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    [Serializable]
    public class BlobPermissionAttribute : CodeAccessSecurityAttribute
    {
        private ILog _log;

        public bool Authenticated
        {
            get { return _authenticated; }
            set { _authenticated = value; }
        }
        private bool _authenticated = true;

        public string Operation { get; set; }
        public string Resource { get; set; }


        public BlobPermissionAttribute(SecurityAction action)
            : base(action)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobPermissionAttribute");
        }

        public override IPermission CreatePermission()
        {
            return Unrestricted
                ? new BlobPermission(PermissionState.Unrestricted)
                : new BlobPermission(Resource, Operation, Authenticated);
        }
    }
}
