using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Blob.Contracts;
using Blob.Contracts.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Before.Infrastructure.Extensions
{
    public static class BlobSecurityUtils
    {
        public static IdentityResultDto ToDto(this IdentityResult result)
        {
            return new IdentityResultDto { Succeeded = result.Succeeded, Errors = result.Errors };
        }

        public static IdentityResult ToResult(this IdentityResultDto dto)
        {
            return (dto.Succeeded)
                ? IdentityResult.Success
                : IdentityResult.Failed(dto.Errors.ToArray());
        }

        public static SignInStatusDto ToDto(this SignInStatus result)
        {
            return EnumUtils.ParseEnum<SignInStatusDto>(result.ToString());
        }

        public static SignInStatus ToResult(this SignInStatusDto dto)
        {
            return EnumUtils.ParseEnum<SignInStatus>(dto.ToString());
        }

        public static Guid GetCustomerId(this IIdentity identity)
        {
            ClaimsIdentity claimsId = (ClaimsIdentity)identity;
            return claimsId.HasClaim(x => x.Type.Equals(SecurityConstants.CustomerIdClaimType)) 
                ? Guid.Parse(claimsId.FindFirstValue(SecurityConstants.CustomerIdClaimType)) 
                : Guid.Empty;
        }
    }
}
