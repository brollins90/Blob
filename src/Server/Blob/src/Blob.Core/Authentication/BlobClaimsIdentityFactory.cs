namespace Blob.Core.Authentication
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Contracts;
    using Identity;
    using log4net;
    using Models;

    public class BlobClaimsIdentityFactory
    {
        private ILog _log;

        public BlobClaimsIdentityFactory()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobClaimsIdentityFactory");
        }

        public Task<ClaimsIdentity> CreateAsync(BlobUserManager manager, User user, string authenticationType)
        {
            _log.Debug("Creating claims identity");
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            //// Just the minimum here
            var id = new ClaimsIdentity(authenticationType, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            //// Id
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            //// Name
            id.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName, ClaimValueTypes.String));
            //// Authenticator Name (us)
            id.AddClaim(new Claim(SecurityConstants.IdentityProviderClaimType, SecurityConstants.IdentityProviderClaimValue, ClaimValueTypes.String));

            //// Allowed customers
            id.AddClaim(new Claim(SecurityConstants.CustomerIdClaimType, user.CustomerId.ToString(), ClaimValueTypes.String));

            ////id.AddClaim(new Claim(SecurityConstants.BlobIdClaimType, user.Id.ToString(), ClaimValueTypes.String));
            ////id.AddClaim(new Claim(SecurityConstants.CustomerIdClaimType, user.CustomerId.ToString(), ClaimValueTypes.String));

            ////id.AddClaim(new Claim(SecurityConstants.IdentityProviderClaimType, SecurityConstants.IdentityProviderClaimValue, ClaimValueTypes.String));




            ////if (manager.SupportsUserSecurityStamp)
            ////{
            ////    id.AddClaim(new Claim(SecurityStampClaimType,
            ////        await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture()));
            ////}
            //if (manager.SupportsUserRole)
            //{
            //    IList<string> roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture();
            //    foreach (string roleName in roles)
            //    {
            //        id.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName, ClaimValueTypes.String));
            //    }
            //}
            ////if (manager.SupportsUserClaim)
            ////{
            ////    id.AddClaims(await manager.GetClaimsAsync(user.Id).WithCurrentCulture());
            ////}
            //
            return Task.FromResult(id);
        }
    }
}