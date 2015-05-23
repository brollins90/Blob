using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Before.Infrastructure.Identity
{
    public interface ISignInManager : IDisposable
    {
        Task<ClaimsIdentity> CreateUserIdentityAsync(UserDto user);
        Task<SignInStatusDto> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
        void SignOut(params string[] authenticationTypes);
    }
}
