
namespace Blob.Contracts
{
    public static class SecurityConstants
    {
        public const string IdentityProviderClaimType = "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";
        public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public const string IdentityProviderClaimValue = "BlobClaims";
        public const string SecurityStampClaimType = "BlobClaims.SecurityStamp";

        public const string BlobIdClaimType = "BlobIdClaim";
        public const string BlobNameClaimType = "BlobNameClaim";
        public const string BlobPrincipalTypeClaimType = "BlobPrincipalTypeClaim";
        public const string BlobRoleClaimType = "BlobRoleClaim";
        public const string CustomerIdClaimType = "CustomerIdClaim";
        public const string DeviceTypeClaimType = "DeviceTypeClaim";
        public const string UserTypeClaimType = "UserTypeClaim";
    }
}
