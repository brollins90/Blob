using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Infrastructure.Extensions;
using Before.Infrastructure.Identity;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Microsoft.AspNet.Identity.Owin;

namespace Before.Controllers
{
    public class AccountController : Controller
    {
        protected IUserManagerService UserManager { get; set; }
        protected ISignInManager SignInManager { get; set; }

        public AccountController(IUserManagerService userManager, ISignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVm model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                SignInStatusDto resultDto = await SignInManager.PasswordSignInAsync(model.UserNameEmail, model.Password, model.RememberMe, shouldLockout: false);
                switch (resultDto.ToResult())
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            return View(model);
        }

        // Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}