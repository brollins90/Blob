using System;
using System.Security;
using System.Security.Permissions;

namespace Blob.Security.Sam
{
    [Serializable]
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BlobAuthorizeAttribute : SecurityAttribute
    {
        public BlobAuthorizeAttribute(SecurityAction action) : base(action) {}
        public override IPermission CreatePermission()
        {
            throw new NotImplementedException();
        }
    }
}
