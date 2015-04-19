////using System;
////using Blob.Data;
////using Microsoft.AspNet.Identity;
//////using Microsoft.AspNet.Identity.EntityFramework;
//////using Microsoft.AspNet.Identity.Owin;
////using Microsoft.Owin;
////using System.Threading.Tasks;

////namespace Blob.Security
////{
////    public class BlobIdentityManager : UserManager<BlobUser>, IIdentityManager<BlobUser, string>
////    {
////        public BlobIdentityManager(IUserStore<BlobUser> store) : base(store) { }

////        //public static BlobUserManager Create(
////        //        IdentityFactoryOptions<BlobUserManager> options,
////        //        IOwinContext context)
////        //{
////        //    var manager =
////        //            new BlobUserManager(
////        //                    new BlobIdentityStore<BlobUser>(
////        //                            context.Get<BlobDbContext>()));

////        //    // Configure validation logic for usernames
////        //    manager.UserValidator = new UserValidator<BlobUser>(manager)
////        //    {
////        //        AllowOnlyAlphanumericUserNames = false,
////        //        RequireUniqueEmail = true
////        //    };
////        //    // Configure validation logic for passwords
////        //    manager.PasswordValidator = new PasswordValidator
////        //    {
////        //        RequiredLength = 6,
////        //        RequireNonLetterOrDigit = true,
////        //        RequireDigit = true,
////        //        RequireLowercase = true,
////        //        RequireUppercase = true,
////        //    };
////        //    var dataProtectionProvider = options.DataProtectionProvider;
////        //    if (dataProtectionProvider != null)
////        //    {
////        //        manager.UserTokenProvider =
////        //                new DataProtectorTokenProvider<BlobUser>(
////        //                        dataProtectionProvider.Create("ASP.NET Identity"));
////        //    }
////        //    return manager;
////        //}

        
////    }
////}



//        // IUserPasswordStore methods
//        private IUserPasswordStore<TUser, TKey> GetPasswordStore()
//        {
//            var cast = Store as IUserPasswordStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserPasswordStore);
//            }
//            return cast;
//        }


//        protected virtual async Task<IdentityResult> UpdatePassword(IUserPasswordStore<TUser, TKey> passwordStore,
//            TUser user, string newPassword)
//        {
//            var result = await PasswordValidator.ValidateAsync(newPassword).WithCurrentCulture();
//            if (!result.Succeeded)
//            {
//                return result;
//            }
//            await
//                passwordStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword)).WithCurrentCulture();
//            await UpdateSecurityStampInternal(user).WithCurrentCulture();
//            return IdentityResult.Success;
//        }

//        /// <summary>
//        ///     By default, retrieves the hashed password from the user store and calls PasswordHasher.VerifyHashPassword
//        /// </summary>
//        /// <param name="store"></param>
//        /// <param name="user"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        protected virtual async Task<bool> VerifyPasswordAsync(IUserPasswordStore<TUser, TKey> store, TUser user,
//            string password)
//        {
//            var hash = await store.GetPasswordHashAsync(user).WithCurrentCulture();
//            return PasswordHasher.VerifyHashedPassword(hash, password) != PasswordVerificationResult.Failed;
//        }

//        // IUserSecurityStampStore methods
//        private IUserSecurityStampStore<TUser, TKey> GetSecurityStore()
//        {
//            var cast = Store as IUserSecurityStampStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserSecurityStampStore);
//            }
//            return cast;
//        }


//        // Update the security stamp if the store supports it
//        internal async Task UpdateSecurityStampInternal(TUser user)
//        {
//            if (SupportsUserSecurityStamp)
//            {
//                await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
//            }
//        }

//        private static string NewSecurityStamp()
//        {
//            return Guid.NewGuid().ToString();
//        }

//        // IUserLoginStore methods
//        private IUserLoginStore<TUser, TKey> GetLoginStore()
//        {
//            var cast = Store as IUserLoginStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserLoginStore);
//            }
//            return cast;
//        }


//        // IUserClaimStore methods
//        private IUserClaimStore<TUser, TKey> GetClaimStore()
//        {
//            var cast = Store as IUserClaimStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserClaimStore);
//            }
//            return cast;
//        }


//        private IUserRoleStore<TUser, TKey> GetUserRoleStore()
//        {
//            var cast = Store as IUserRoleStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserRoleStore);
//            }
//            return cast;
//        }


//        // IUserEmailStore methods
//        internal IUserEmailStore<TUser, TKey> GetEmailStore()
//        {
//            var cast = Store as IUserEmailStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserEmailStore);
//            }
//            return cast;
//        }


//        // IUserPhoneNumberStore methods
//        internal IUserPhoneNumberStore<TUser, TKey> GetPhoneNumberStore()
//        {
//            var cast = Store as IUserPhoneNumberStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserPhoneNumberStore);
//            }
//            return cast;
//        }


//        internal async Task<SecurityToken> CreateSecurityTokenAsync(TKey userId)
//        {
//            return
//                new SecurityToken(Encoding.Unicode.GetBytes(await GetSecurityStampAsync(userId).WithCurrentCulture()));
//        }


//        // IUserFactorStore methods
//        internal IUserTwoFactorStore<TUser, TKey> GetUserTwoFactorStore()
//        {
//            var cast = Store as IUserTwoFactorStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserTwoFactorStore);
//            }
//            return cast;
//        }


//        // IUserLockoutStore methods
//        internal IUserLockoutStore<TUser, TKey> GetUserLockoutStore()
//        {
//            var cast = Store as IUserLockoutStore<TUser, TKey>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserLockoutStore);
//            }
//            return cast;
//        }


//        private void ThrowIfDisposed()
//        {
//            if (_disposed)
//            {
//                throw new ObjectDisposedException(GetType().Name);
//            }
//        }

//        /// <summary>
//        ///     When disposing, actually dipose the store
//        /// </summary>
//        /// <param name="disposing"></param>
//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing && !_disposed)
//            {
//                Store.Dispose();
//                _disposed = true;
//            }
//        }

