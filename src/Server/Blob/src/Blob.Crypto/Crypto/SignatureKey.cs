namespace Blob.Crypto.Crypto
{
    using System;

    public class SignatureKey : CryptKey
    {
        internal SignatureKey(CryptContext ctx, IntPtr handle) : base(ctx, handle) { }

        public override KeyType Type
        {
            get { return KeyType.Signature; }
        }
    }
}