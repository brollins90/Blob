using System;
using System.Linq;
using Blob.Contracts.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Before.Infrastructure.Extensions
{
    public static class BlobSecurityUtils
    {

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

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
            return ParseEnum<SignInStatusDto>(result.ToString());
        }

        public static SignInStatus ToResult(this SignInStatusDto dto)
        {
            return ParseEnum<SignInStatus>(dto.ToString());
        }
    }
}