using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace Blob.Contracts.Security
{
    public interface ISignInManagerService<TUser, TKey> : IDisposable
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        IAuthenticationManagerService AuthenticationManager { get; set; }
        string AuthenticationType { get; set; }
        IUserManagerService<TUser, TKey> UserManager { get; set; }
        TKey ConvertIdFromString(string id);
        string ConvertIdToString(TKey id);
        Task<System.Security.Claims.ClaimsIdentity> CreateUserIdentityAsync(TUser user); 
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);
        Task<TKey> GetVerifiedUserIdAsync();
        Task<bool> HasBeenVerifiedAsync();
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
        Task<bool> SendTwoFactorCodeAsync(string provider);
        Task SignInAsync(TUser user, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
    }
}
