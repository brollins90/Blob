using System;

namespace Blob.Core.Domain
{
    public class KeyPair
    {
        public Guid Id { get; set; }
        public string AssociatedEntity { get; set; }
        public virtual byte[] PrivateKey { get; set; }
        public virtual byte[] PublicKey { get; set; }
    }
}
