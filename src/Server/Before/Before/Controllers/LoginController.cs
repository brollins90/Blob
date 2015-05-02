//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using Before.Infrastructure.Extensions;
//using Before.Infrastructure.Identity;
//using Before.Models;
//using Blob.Contracts.Security;
//using log4net;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;

//namespace Before.Controllers
//{
//    [Authorize]
//    [RoutePrefix("login")]
//    [Route("{action=login}")]
//    public class LoginController : IdentityController
//    {
//        public LoginController(BeforeUserManager userManager, BeforeSignInManager signInManager, ILog log)
//            : base(userManager, signInManager, log) { }

//        //
//        // GET: /Login/Login
//        [AllowAnonymous]
//        [Route("login")]
//        public ActionResult Login(string returnUrl)
//        {
//            ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }

//        //
//        // POST: /Login/Login
//        [AllowAnonymous]
//        [HttpPost]
//        [Route("login")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            SignInStatusDto resultDto = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
//            switch (resultDto.ToResult())
//            {
//                case SignInStatus.Success:
//                    return RedirectToLocal(returnUrl);
//                case SignInStatus.LockedOut:
//                    return View("Lockout");
//                //case SignInStatus.RequiresVerification:
//                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
//                case SignInStatus.Failure:
//                default:
//                    ModelState.AddModelError("", "Invalid login attempt.");
//                    return View(model);
//            }
//        }

//        //
//        // POST: /Login/Logout
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Route("logout")]
//        public ActionResult LogOut()
//        {
//            AuthenticationManager.SignOut();
//            return RedirectToAction("Index", "Home");
//        }

//        ////
//        //// POST: /Login/ExternalLogin
//        //[AllowAnonymous]
//        //[HttpPost]
//        //[Route("logout")]
//        //[ValidateAntiForgeryToken]
//        //public ActionResult ExternalLogin(string provider, string returnUrl)
//        //{
//        //    // Request a redirect to the external login provider
//        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Login", new { ReturnUrl = returnUrl }));
//        //}

//        //
//        // GET: /Account/ExternalLoginCallback
//        //[AllowAnonymous]
//        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
//        //{
//        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
//        //    if (loginInfo == null)
//        //    {
//        //        return RedirectToAction("Login");
//        //    }

//        //    // Sign in the user with this external login provider if the user already has a login
//        //    var result = await UserManager.ExternalSignInAsync(loginInfo, isPersistent: false);
//        //    switch (result)
//        //    {
//        //        case SignInStatus.Success:
//        //            return RedirectToLocal(returnUrl);
//        //        case SignInStatus.LockedOut:
//        //            return View("Lockout");
//        //        case SignInStatus.RequiresVerification:
//        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
//        //        case SignInStatus.Failure:
//        //        default:
//        //            // If the user does not have an account, then prompt the user to create an account
//        //            ViewBag.ReturnUrl = returnUrl;
//        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
//        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
//        //    }
//        //}

//        //
//        // POST: /Account/ExternalLoginConfirmation
//        //[HttpPost]
//        //[AllowAnonymous]
//        //[ValidateAntiForgeryToken]
//        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
//        //{
//        //    if (User.Identity.IsAuthenticated)
//        //    {
//        //        return RedirectToAction("Index", "Manage");
//        //    }

//        //    if (ModelState.IsValid)
//        //    {
//        //        // Get the information about the user from the external login provider
//        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
//        //        if (info == null)
//        //        {
//        //            return View("ExternalLoginFailure");
//        //        }
//        //        UserDto user = new ApplicationUser { UserName = model.Email, Email = model.Email };
//        //        var result = await UserManager.CreateAsync(user);
//        //        if (result.Succeeded)
//        //        {
//        //            result = await UserManager.AddLoginAsync(user.Id, new UserLoginInfoDto(info.Login.LoginProvider, info.Login.ProviderKey));
//        //            if (result.Succeeded)
//        //            {
//        //                await UserManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
//        //                return RedirectToLocal(returnUrl);
//        //            }
//        //        }
//        //        AddErrors(result);
//        //    }

//        //    ViewBag.ReturnUrl = returnUrl;
//        //    return View(model);
//        //}

//        ////
//        //// GET: /Account/ExternalLoginFailure
//        //[AllowAnonymous]
//        //public ActionResult ExternalLoginFailure()
//        //{
//        //    return View();
//        //}
//    }



//    internal class ChallengeResult : HttpUnauthorizedResult
//    {
//        public ChallengeResult(string provider, string redirectUri)
//        {
//        }

//        public ChallengeResult(string provider, string redirectUri, string userId)
//        {
//            LoginProvider = provider;
//            RedirectUri = redirectUri;
//            UserId = userId;
//        }

//        public string LoginProvider { get; set; }
//        public string RedirectUri { get; set; }
//        public string UserId { get; set; }

//        public override void ExecuteResult(ControllerContext context)
//        {
//            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
//            if (UserId != null)
//            {
//                properties.Dictionary[XsrfKey] = UserId;
//            }
//            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
//        }

//        // Used for XSRF protection when adding external logins
//        protected const string XsrfKey = "XsrfId";
//    }
//}