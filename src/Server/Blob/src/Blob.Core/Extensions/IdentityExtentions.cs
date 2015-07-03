namespace Blob.Core.Extensions
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using Contracts;
    using Contracts.Models;
    using Microsoft.AspNet.Identity;
    using Models;

    public static class UserLoginExtensions
    {
        public static UserLoginInfo ToLoginInfo(this UserLoginInfoDto res)
        {
            return new UserLoginInfo(res.LoginProvider, res.ProviderKey);
        }

        public static UserLoginInfoDto ToDto(this UserLoginInfo res)
        {
            return new UserLoginInfoDto { LoginProvider = res.LoginProvider, ProviderKey = res.ProviderKey };
        }
    }

    public static class BlobSecurityExtentions
    {

        public static UserDto ToDto(this User user)
        {
            return new UserDto { Id = user.Id.ToString(), UserName = user.UserName };
        }

        public static User ToUser(this UserDto user)
        {
            return new User { Id = Guid.Parse(user.Id), UserName = user.UserName };
        }

        public static Guid ToGuid(this string s)
        {
            return Guid.Parse(s);
        }

        public static string GetBlobId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue(SecurityConstants.BlobIdClaimType);
            }
            return null;
        }

        //public static string GetName(this IIdentity identity)
        //{
        //    if (identity == null)
        //    {
        //        throw new ArgumentNullException("identity");
        //    }
        //    var ci = identity as ClaimsIdentity;
        //    if (ci != null)
        //    {
        //        return ci.FindFirstValue(SecurityConstants.BlobName);
        //    }
        //    return null;
        //}

        public static string GetCustomerId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue(SecurityConstants.CustomerIdClaimType);
            }
            return null;
        }
    }
}