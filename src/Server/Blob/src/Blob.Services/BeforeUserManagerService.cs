using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Identity;
using log4net;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blob.Services
{
    public class BeforeUserManagerService : IUserManagerService
    {
        private ILog _log;
        private readonly BlobUserManager _manager;
        private bool _disposed;

        public BeforeUserManagerService(BlobUserManager manager)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobUserManagerAdapter");
            _manager = manager;
        }

        public bool UserLockoutEnabledByDefault
        {
            get
            {
                ThrowIfDisposed();
                return _manager.UserLockoutEnabledByDefault;
            }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get
            {
                ThrowIfDisposed();
                return _manager.MaxFailedAccessAttemptsBeforeLockout;
            }
        }

        public TimeSpan DefaultAccountLockoutTimeSpan
        {
            get
            {
                ThrowIfDisposed();
                return _manager.DefaultAccountLockoutTimeSpan;
            }
        }

        public bool SupportsUserTwoFactor
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserTwoFactor;
            }
        }

        public bool SupportsUserPassword
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserPassword;
            }
        }

        public bool SupportsUserSecurityStamp
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserSecurityStamp;
            }
        }

        public bool SupportsUserRole
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserRole;
            }
        }

        public bool SupportsUserLogin
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserLogin;
            }
        }

        public bool SupportsUserEmail
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserEmail;
            }
        }

        public bool SupportsUserPhoneNumber
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserPhoneNumber;
            }
        }

        public bool SupportsUserClaim
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserClaim;
            }
        }

        public bool SupportsUserLockout
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsUserLockout;
            }
        }

        public bool SupportsQueryableUsers
        {
            get
            {
                ThrowIfDisposed();
                return _manager.SupportsQueryableUsers;
            }
        }

        #region IUserEmailStoreService

        public Task<UserDto> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            return _manager.FindByEmailAsync(email);
        }

        public Task<string> GetEmailAsync(string userId)
        {
            ThrowIfDisposed();
            return _manager.GetEmailAsync(Guid.Parse(userId));
        }

        public Task<bool> GetEmailConfirmedAsync(string userId)
        {
            ThrowIfDisposed();
            return _manager.GetEmailConfirmedAsync(Guid.Parse(userId));
        }

        public Task SetEmailAsync(string userId, string email)
        {
            ThrowIfDisposed();
            return _manager.SetEmailAsync(Guid.Parse(userId), email);
        }

        public Task SetEmailConfirmedAsync(string userId, bool confirmed)
        {
            ThrowIfDisposed();
            return _manager.SetEmailConfirmedAsync(Guid.Parse(userId), confirmed);
        }

        #endregion

        #region IUserPasswordStoreService

        public Task<string> GetPasswordHashAsync(string userId)
        {
            ThrowIfDisposed();
            return _manager.GetPasswordHashAsync(Guid.Parse(userId));
        }

        public Task<bool> HasPasswordAsync(string userId)
        {
            ThrowIfDisposed();
            return _manager.HasPasswordAsync(Guid.Parse(userId));
        }

        public Task SetPasswordHashAsync(string userId, string passwordHash)
        {
            ThrowIfDisposed();
            return _manager.SetPasswordHashAsync(Guid.Parse(userId), passwordHash);
        }

        #endregion

        #region IUserRoleStoreService

        public Task AddToRoleAsync(string userId, string role)
        {
            ThrowIfDisposed();
            return _manager.AddToRoleAsync(Guid.Parse(userId), role);
        }

        public Task AddToRolesAsync(string userId, string[] roles)
        {
            ThrowIfDisposed();
            return _manager.AddToRolesAsync(Guid.Parse(userId), roles);
        }

        public Task<IList<string>> GetRolesAsync(string userId)
        {
            ThrowIfDisposed();
            return _manager.GetRolesAsync(Guid.Parse(userId));
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            ThrowIfDisposed();
            return _manager.IsInRoleAsync(Guid.Parse(userId), role);
        }

        public Task RemoveFromRoleAsync(string userId, string role)
        {
            ThrowIfDisposed();
            return _manager.RemoveFromRoleAsync(Guid.Parse(userId), role);
        }

        #endregion

        #region IUserStoreService

        public Task CreateAsync(UserDto user)
        {
            ThrowIfDisposed();
            return _manager.CreateAsync(user);
        }

        public Task DeleteAsync(string userId)
        {
            ThrowIfDisposed();
            return _manager.DeleteAsync(Guid.Parse(userId));
        }

        public Task<UserDto> FindByIdAsync(string userId)
        {
            ThrowIfDisposed();
            return _manager.FindByIdAsync(Guid.Parse(userId));
        }

        public Task<UserDto> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            return _manager.FindByNameAsync(userName);
        }

        public Task UpdateAsync(UserDto user)
        {
            ThrowIfDisposed();
            return _manager.UpdateAsync(user);
        }

        #endregion

        public Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType)
        {
            ThrowIfDisposed();
            return _manager.CreateIdentityAsync(user, authenticationType);
        }

        public Task<IdentityResultDto> CreateAsync(UserDto user, string password)
        {
            ThrowIfDisposed();
            return _manager.CreateAsync(user, password);
        }

        public Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            ThrowIfDisposed();
            return _manager.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        ///     When disposing, actually dipose the store
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (!_manager.IsDisposed)
                    _manager.Dispose();
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
