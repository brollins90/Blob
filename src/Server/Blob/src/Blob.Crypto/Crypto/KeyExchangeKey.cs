namespace Blob.Crypto.Crypto
{
    using System;

    public class KeyExchangeKey : CryptKey
    {
        internal KeyExchangeKey(CryptContext ctx, IntPtr handle) : base(ctx, handle) { }

        public override KeyType Type
        {
            get { return KeyType.Exchange; }
        }
    }
}