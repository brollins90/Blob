using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Security.Claims;
using Blob.Core.Domain;
using log4net;

namespace Blob.Security.Identity
{
    public class BlobClaimsIdentityFactory
    {
        private ILog _log;
        internal const string IdentityProviderClaimType = "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        internal const string DefaultIdentityProviderClaimValue = "ASP.NET Identity";
        public const string DefaultSecurityStampClaimType = "AspNet.Identity.SecurityStamp";

        /// <summary>
        ///     Constructor
        /// </summary>
        public BlobClaimsIdentityFactory()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobClaimsIdentityFactory");

            RoleClaimType = ClaimsIdentity.DefaultRoleClaimType;
            UserIdClaimType = ClaimTypes.NameIdentifier;
            UserNameClaimType = ClaimsIdentity.DefaultNameClaimType;
            SecurityStampClaimType = DefaultSecurityStampClaimType;
        }

        /// <summary>
        ///     Claim type used for role claims
        /// </summary>
        public string RoleClaimType { get; set; }

        /// <summary>
        ///     Claim type used for the user name
        /// </summary>
        public string UserNameClaimType { get; set; }

        /// <summary>
        ///     Claim type used for the user id
        /// </summary>
        public string UserIdClaimType { get; set; }

        /// <summary>
        ///     Claim type used for the user security stamp
        /// </summary>
        public string SecurityStampClaimType { get; set; }

        /// <summary>
        ///     Create a ClaimsIdentity from a user
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<ClaimsIdentity> CreateAsync(BlobUserManager manager, User user, string authenticationType)
        {
            _log.Debug("CreateAsync");
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var id = new ClaimsIdentity(authenticationType, UserNameClaimType, RoleClaimType);
            id.AddClaim(new Claim(UserIdClaimType, user.Id.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(UserNameClaimType, user.UserName, ClaimValueTypes.String));
            id.AddClaim(new Claim(IdentityProviderClaimType, DefaultIdentityProviderClaimValue, ClaimValueTypes.String));
            //if (manager.SupportsUserSecurityStamp)
            //{
            //    id.AddClaim(new Claim(SecurityStampClaimType,
            //        await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture()));
            //}
            if (manager.SupportsUserRole)
            {
                IList<string> roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture();
                foreach (string roleName in roles)
                {
                    id.AddClaim(new Claim(RoleClaimType, roleName, ClaimValueTypes.String));
                }
            }
            //if (manager.SupportsUserClaim)
            //{
            //    id.AddClaims(await manager.GetClaimsAsync(user.Id).WithCurrentCulture());
            //}
            return id;
        }
    }
}
