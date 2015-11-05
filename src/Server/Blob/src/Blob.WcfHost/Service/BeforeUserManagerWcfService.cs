using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.WcfHost.Service
{
    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class BeforeUserManagerWcfService : IUserManagerService
    {
        private IUserManagerService _userManagerService;

        public BeforeUserManagerWcfService(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        public TimeSpan DefaultAccountLockoutTimeSpan
        {
            get
            {
                return _userManagerService.DefaultAccountLockoutTimeSpan;
            }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get
            {
                return _userManagerService.MaxFailedAccessAttemptsBeforeLockout;
            }
        }

        public bool SupportsQueryableUsers
        {
            get
            {
                return _userManagerService.SupportsQueryableUsers;
            }
        }

        public bool SupportsUserClaim
        {
            get
            {
                return _userManagerService.SupportsUserClaim;
            }
        }

        public bool SupportsUserEmail
        {
            get
            {
                return _userManagerService.SupportsUserEmail;
            }
        }

        public bool SupportsUserLockout
        {
            get
            {
                return _userManagerService.SupportsUserLockout;
            }
        }

        public bool SupportsUserLogin
        {
            get
            {
                return _userManagerService.SupportsUserLogin;
            }
        }

        public bool SupportsUserPassword
        {
            get
            {
                return _userManagerService.SupportsUserPassword;
            }
        }

        public bool SupportsUserPhoneNumber
        {
            get
            {
                return _userManagerService.SupportsUserPhoneNumber;
            }
        }

        public bool SupportsUserRole
        {
            get
            {
                return _userManagerService.SupportsUserRole;
            }
        }

        public bool SupportsUserSecurityStamp
        {
            get
            {
                return _userManagerService.SupportsUserSecurityStamp;
            }
        }

        public bool SupportsUserTwoFactor
        {
            get
            {
                return _userManagerService.SupportsUserTwoFactor;
            }
        }

        public bool UserLockoutEnabledByDefault
        {
            get
            {
                return _userManagerService.UserLockoutEnabledByDefault;
            }
        }

        public async Task AddToRoleAsync(string userId, string roleName)
        {
            await _userManagerService.AddToRoleAsync(userId, roleName);
        }

        public async Task AddToRolesAsync(string userId, string[] roleNames)
        {
            await _userManagerService.AddToRolesAsync(userId, roleNames);
        }

        public async Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            return await _userManagerService.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        public async Task CreateAsync(UserDto user)
        {
            await _userManagerService.CreateAsync(user);
        }

        public async Task<IdentityResultDto> CreateAsync(UserDto user, string password)
        {
            return await _userManagerService.CreateAsync(user, password);
        }

        public async Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType)
        {
            return await _userManagerService.CreateIdentityAsync(user, authenticationType);
        }

        public async Task DeleteAsync(string userId)
        {
            await _userManagerService.DeleteAsync(userId);
        }

        public void Dispose()
        {
            _userManagerService.Dispose();
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            return await _userManagerService.FindByEmailAsync(email);
        }

        public async Task<UserDto> FindByIdAsync(string userId)
        {
            return await _userManagerService.FindByIdAsync(userId);
        }

        public async Task<UserDto> FindByNameAsync(string userName)
        {
            return await _userManagerService.FindByNameAsync(userName);
        }

        public async Task<string> GetEmailAsync(string userId)
        {
            return await _userManagerService.GetEmailAsync(userId);
        }

        public async Task<bool> GetEmailConfirmedAsync(string userId)
        {
            return await _userManagerService.GetEmailConfirmedAsync(userId);
        }

        public async Task<string> GetPasswordHashAsync(string userId)
        {
            return await _userManagerService.GetPasswordHashAsync(userId);
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            return await _userManagerService.GetRolesAsync(userId);
        }

        public async Task<bool> HasPasswordAsync(string userId)
        {
            return await _userManagerService.HasPasswordAsync(userId);
        }

        public async Task<bool> IsInRoleAsync(string userId, string roleName)
        {
            return await _userManagerService.IsInRoleAsync(userId, roleName);
        }

        public async Task RemoveFromRoleAsync(string userId, string roleName)
        {
            await _userManagerService.RemoveFromRoleAsync(userId, roleName);
        }

        public async Task SetEmailAsync(string userId, string email)
        {
            await _userManagerService.SetEmailAsync(userId, email);
        }

        public async Task SetEmailConfirmedAsync(string userId, bool confirmed)
        {
            await _userManagerService.SetEmailConfirmedAsync(userId, confirmed);
        }

        public async Task SetPasswordHashAsync(string userId, string passwordHash)
        {
            await _userManagerService.SetPasswordHashAsync(userId, passwordHash);
        }

        public async Task UpdateAsync(UserDto user)
        {
            await _userManagerService.UpdateAsync(user);
        }
    }
}