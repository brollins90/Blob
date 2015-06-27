//
// This code was written by Keith Brown, and may be freely used.
// Want to learn more about .NET? Visit pluralsight.com today!
// http://blog.pluralsight.com/selfcert-create-a-self-signed-certificate-interactively-gui-or-programmatically-in-net
//
using System;

namespace Blob.Crypto.Crypto
{
    public abstract class CryptKey : DisposeableObject
    {
        readonly CryptContext _ctx;

        internal IntPtr Handle { get; private set; }

        internal CryptKey(CryptContext ctx, IntPtr handle)
        {
            this._ctx = ctx;
            this.Handle = handle;
        }

        public abstract KeyType Type { get; }

        protected override void CleanUp(bool viaDispose)
        {
            // keys are invalid once CryptContext is closed,
            // so the only time I try to close an individual key is if a user
            // explicitly disposes of the key.
            if (viaDispose)
                _ctx.DestroyKey(this);
        }
    }
}
