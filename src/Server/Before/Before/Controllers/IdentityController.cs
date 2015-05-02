//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using Before.Infrastructure.Identity;
//using log4net;
//using Microsoft.AspNet.Identity;
//using Microsoft.Owin.Security;

//namespace Before.Controllers
//{
//    [Authorize]
//    public abstract class IdentityController : Controller
//    {
//        private ILog _log;
//        protected BeforeUserManager UserManager { get; set; }
//        protected BeforeSignInManager SignInManager { get; set; }

//        public IdentityController(BeforeUserManager userManager, BeforeSignInManager signInManager, ILog log)
//        {
//            _log = log;
//            UserManager = userManager;
//            SignInManager = signInManager;
//        }

//        protected async Task SignInAsync(BeforeUser user, bool isPersistent)
//        {
//            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
//            ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(true);

//            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
//        }

//        #region Helpers

//        protected IAuthenticationManager AuthenticationManager
//        {
//            get { return HttpContext.GetOwinContext().Authentication; }
//        }

//        protected void AddErrors(IdentityResult result)
//        {
//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError("", error);
//            }
//        }

//        protected ActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            return RedirectToAction("Index", "Home");
//        }

//        protected ClaimsIdentity GetBasicUserIdentity(string name)
//        {
//            List<Claim> claims = new List<Claim>
//                         {
//                             new Claim(ClaimTypes.Name, name)
//                         };
//            return new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
//        }

//        #endregion
//    }
//}