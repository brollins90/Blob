﻿//using System;
//using System.Data.Entity.Utilities;
//using System.Globalization;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Blob.Contracts.Security;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;
//using Microsoft.Owin.Security;

//namespace Before.Infrastructure.Identity
//{
//    public class BeforeSignInManager : IDisposable
//    {
//        public BeforeSignInManager(BeforeUserManager userManager)
//        {
//            if (userManager == null)
//            {
//                throw new ArgumentNullException("userManager");
//            }
//            if (authenticationManager == null)
//            {
//                throw new ArgumentNullException("authenticationManager");
//            }
//            UserManager = userManager;
//            AuthenticationManager = authenticationManager;
//        }
//        public static BeforeSignInManager Create(IdentityFactoryOptions<BeforeSignInManager> options, IOwinContext context)
//        {
//            return new BeforeSignInManager(context.GetUserManager<BeforeUserManager>(), context.Authentication);
//        }

//        /// <summary>
//        /// AuthenticationType that will be used by sign in, defaults to DefaultAuthenticationTypes.ApplicationCookie
//        /// </summary>
//        public string AuthenticationType
//        {
//            get { return _authenticationType ?? DefaultAuthenticationTypes.ApplicationCookie; }
//            set { _authenticationType = value; }
//        }

//        private string _authenticationType;

//        public BeforeUserManager UserManager { get; set; }
//        public IAuthenticationManager AuthenticationManager { get; set; }

//        public virtual Task<ClaimsIdentity> CreateUserIdentityAsync(UserDto user)
//        {
//            return UserManager.CreateIdentityAsync(user, AuthenticationType);
//        }

//        public string ConvertIdToString(Guid id)
//        {
//            return Convert.ToString(id, CultureInfo.InvariantCulture);
//        }

//        public Guid ConvertIdFromString(string id)
//        {
//            if (id == null)
//            {
//                return default(Guid);
//            }
//            return (Guid)Convert.ChangeType(id, typeof(Guid), CultureInfo.InvariantCulture);
//        }


//        //public virtual async Task<bool> SendTwoFactorCodeAsync(string provider)
//        //{
//        //    var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
//        //    if (userId == null)
//        //    {
//        //        return false;
//        //    }

//        //    var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture();
//        //    // See IdentityConfig.cs to plug in Email/SMS services to actually send the code
//        //    await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture();
//        //    return true;
//        //}

//        public async Task<Guid> GetVerifiedUserIdAsync()
//        {
//            var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie).WithCurrentCulture();
//            if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId()))
//            {
//                return ConvertIdFromString(result.Identity.GetUserId());
//            }
//            return default(Guid);
//        }

//        /// <summary>
//        /// Has the user been verified (ie either via password or external login)
//        /// </summary>
//        /// <returns></returns>
//        public async Task<bool> HasBeenVerifiedAsync()
//        {
//            return await GetVerifiedUserIdAsync().WithCurrentCulture() != null;
//        }

//        ///// <summary>
//        ///// Two factor verification step
//        ///// </summary>
//        ///// <param name="provider"></param>
//        ///// <param name="code"></param>
//        ///// <param name="isPersistent"></param>
//        ///// <param name="rememberBrowser"></param>
//        ///// <returns></returns>
//        //public virtual async Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
//        //{
//        //    //Guid userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
//        //    //if (userId == null)
//        //    //{
//        //    //    return SignInStatus.Failure;
//        //    //}
//        //    //UserDto user = await UserManager.FindByIdAsync(ConvertIdToString(userId)).WithCurrentCulture();
//        //    //if (user == null)
//        //    //{
//        //    //    return SignInStatus.Failure;
//        //    //}
//        //    //if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
//        //    //{
//        //    //    return SignInStatus.LockedOut;
//        //    //}
//        //    //if (await UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture())
//        //    //{
//        //    //    // When token is verified correctly, clear the access failed count used for lockout
//        //    //    await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
//        //    //    await SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
//        //    //    return SignInStatus.Success;
//        //    //}
//        //    //// If the token is incorrect, record the failure which also may cause the user to be locked out
//        //    //await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
//        //    return SignInStatus.Failure;
//        //}

//        /// <summary>
//        /// Sign the user in using an associated external login
//        /// </summary>
//        /// <param name="loginInfo"></param>
//        /// <param name="isPersistent"></param>
//        /// <returns></returns>
//        public async Task<SignInStatusDto> ExternalSignInAsync(ExternalLoginInfoDto loginInfo, bool isPersistent)
//        {
//            var user = await UserManager.FindAsync(loginInfo.Login).WithCurrentCulture();
//            //if (user == null)
//            //{
//            //    return SignInStatus.Failure;
//            //}
//            //if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
//            //{
//            //    return SignInStatus.LockedOut;
//            //}
//            return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
//        }

//        private async Task<SignInStatusDto> SignInOrTwoFactor(UserDto user, bool isPersistent)
//        {
//            string id = Convert.ToString(user.Id);
//            //if (await UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture()
//            //    && (await UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture()).Count > 0
//            //    && !await AuthenticationManager.TwoFactorBrowserRememberedAsync(id).WithCurrentCulture())
//            //{
//            //    var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
//            //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
//            //    AuthenticationManager.SignIn(identity);
//            //    return SignInStatus.RequiresVerification;
//            //}
//            await SignInAsync(user, isPersistent, false).WithCurrentCulture();
//            return SignInStatusDto.Success;
//        }


//        /// <summary>
//        ///     Dispose
//        /// </summary>
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        /// <summary>
//        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
//        /// </summary>
//        /// <param name="disposing"></param>
//        protected virtual void Dispose(bool disposing) { }
//    }
//}