namespace Blob.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Contracts.Models;
    using Contracts.ServiceContracts;

    public class IdentityManagerClient : BaseClient<IUserManagerService>, IUserManagerService
    {
        public IdentityManagerClient(string endpointName, string username, string password)
            : base(endpointName, username, password)
        { }

        protected internal bool IsDisposed { get; private set; }

        public bool UserLockoutEnabledByDefault
        {
            get
            {
                try
                {
                    return Channel.UserLockoutEnabledByDefault;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get
            {
                try
                {
                    return Channel.MaxFailedAccessAttemptsBeforeLockout;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(int);
            }
        }

        public TimeSpan DefaultAccountLockoutTimeSpan
        {
            get
            {
                try
                {
                    return Channel.DefaultAccountLockoutTimeSpan;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(TimeSpan);
            }
        }

        public bool SupportsUserTwoFactor
        {
            get
            {
                try
                {
                    return Channel.SupportsUserTwoFactor;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserPassword
        {
            get
            {
                try
                {
                    return Channel.SupportsUserPassword;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserSecurityStamp
        {
            get
            {
                try
                {
                    return Channel.SupportsUserSecurityStamp;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserRole
        {
            get
            {
                try
                {
                    return Channel.SupportsUserRole;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserLogin
        {
            get
            {
                try
                {
                    return Channel.SupportsUserLogin;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserEmail
        {
            get
            {
                try
                {
                    return Channel.SupportsUserEmail;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserPhoneNumber
        {
            get
            {
                try
                {
                    return Channel.SupportsUserPhoneNumber;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserClaim
        {
            get
            {
                try
                {
                    return Channel.SupportsUserClaim;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserLockout
        {
            get
            {
                try
                {
                    return Channel.SupportsUserLockout;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsQueryableUsers
        {
            get
            {
                try
                {
                    return Channel.SupportsQueryableUsers;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            try
            {
                return await Channel.FindByEmailAsync(email).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<string> GetEmailAsync(string userId)
        {
            try
            {
                return await Channel.GetEmailAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<bool> GetEmailConfirmedAsync(string userId)
        {
            try
            {
                return await Channel.GetEmailConfirmedAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return default(bool);
        }

        public async Task SetEmailAsync(string userId, string email)
        {
            try
            {
                await Channel.SetEmailAsync(userId, email).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task SetEmailConfirmedAsync(string userId, bool confirmed)
        {
            try
            {
                await Channel.SetEmailConfirmedAsync(userId, confirmed).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<string> GetPasswordHashAsync(string userId)
        {
            try
            {
                return await Channel.GetPasswordHashAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<bool> HasPasswordAsync(string userId)
        {
            try
            {
                return await Channel.HasPasswordAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return false;
        }

        public async Task SetPasswordHashAsync(string userId, string passwordHash)
        {
            try
            {
                await Channel.SetPasswordHashAsync(userId, passwordHash).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task AddToRoleAsync(string userId, string role)
        {
            try
            {
                await Channel.AddToRoleAsync(userId, role).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task AddToRolesAsync(string userId, string[] roles)
        {
            try
            {
                await Channel.AddToRolesAsync(userId, roles).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            try
            {
                return await Channel.GetRolesAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            try
            {
                return await Channel.IsInRoleAsync(userId, role).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return false;
        }

        public async Task RemoveFromRoleAsync(string userId, string role)
        {
            try
            {
                await Channel.RemoveFromRoleAsync(userId, role).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task CreateAsync(UserDto user)
        {
            try
            {
                await Channel.CreateAsync(user).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task DeleteAsync(string userId)
        {
            try
            {
                await Channel.DeleteAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<UserDto> FindByIdAsync(string userId)
        {
            try
            {
                return await Channel.FindByIdAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserDto> FindByNameAsync(string userName)
        {
            try
            {
                return await Channel.FindByNameAsync(userName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task UpdateAsync(UserDto user)
        {
            try
            {
                await Channel.UpdateAsync(user).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }


        public async Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType)
        {
            try
            {
                return await Channel.CreateIdentityAsync(user, authenticationType).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<IdentityResultDto> CreateAsync(UserDto user, string password)
        {
            try
            {
                return await Channel.CreateAsync(user, password).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            try
            {
                return await Channel.ChangePasswordAsync(userId, currentPassword, newPassword).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                IsDisposed = true;
            }
        }
    }
}