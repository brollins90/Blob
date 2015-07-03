namespace Blob.Crypto.Crypto
{
    using System;

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