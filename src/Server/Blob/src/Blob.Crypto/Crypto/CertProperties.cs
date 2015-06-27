//
// This code was written by Keith Brown, and may be freely used.
// Want to learn more about .NET? Visit pluralsight.com today!
// http://blog.pluralsight.com/selfcert-create-a-self-signed-certificate-interactively-gui-or-programmatically-in-net
//
using System;
using System.Security.Cryptography.X509Certificates;

namespace Blob.Crypto.Crypto
{
    public class CertProperties
    {
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public X500DistinguishedName Name { get; set; }
        public int KeyBitLength { get; set; }
        public bool IsPrivateKeyExportable { get; set; }

        public CertProperties()
        {
            DateTime today = DateTime.Today;
            ValidFrom = today.AddDays(-1);
            ValidTo = today.AddYears(10);
            Name = new X500DistinguishedName("cn=self");
            KeyBitLength = 4096;
        }
    }
}
