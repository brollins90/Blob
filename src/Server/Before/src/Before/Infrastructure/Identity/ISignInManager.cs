namespace Before.Infrastructure.Identity
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Blob.Contracts.Models;

    public interface ISignInManager : IDisposable
    {
        Task<ClaimsIdentity> CreateUserIdentityAsync(UserDto user);
        Task<SignInStatusDto> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
        void SignOut(params string[] authenticationTypes);
    }
}