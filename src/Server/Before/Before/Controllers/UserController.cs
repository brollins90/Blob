using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Infrastructure.Extensions;
using Before.Infrastructure.Identity;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        public UserController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager, IUserManagerService userManager, BeforeSignInManager signInManager)
            : base(blobCommandManager, blobQueryManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        protected IUserManagerService UserManager { get; set; }
        protected BeforeSignInManager SignInManager { get; set; }

        // GET: /user/disable/{id}
        public async Task<ActionResult> Disable(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetUserDisableVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DisableModal", viewModel);
        }

        // POST: /user/disable/{model}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disable(UserDisableVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DisableUserAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DisableModal", model);
        }

        // GET: /user/edit/{id}
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetUserUpdateVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditModal", viewModel);
        }

        // POST: /user/edit/{model}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateUserAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", model);
        }

        // GET: /user/editpassword/{id}
        public async Task<ActionResult> EditPassword(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetUserUpdatePasswordVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditPasswordModal", viewModel);
        }

        //
        // POST: /user/editpassword/{model}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPassword(UserUpdatePasswordVm model)
        {
            if (ModelState.IsValid)
            {
                IdentityResultDto result = await UserManager.ChangePasswordAsync(model.UserId, model.OldPassword, model.NewPassword).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(model.UserId.ToString()).ConfigureAwait(true);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(true);
                    return Json(new { success = true });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return PartialView("_EditPasswordModal", model);
        }

        // GET: /user/enable/{id}
        public async Task<ActionResult> Enable(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetUserEnableVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EnableModal", viewModel);
        }

        // POST: /user/enable/{model}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enable(UserEnableVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.EnableUserAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EnableModal", model);
        }

        // GET: /user/PageForCustomer/{customerId}
        public ActionResult PageForCustomer(Guid id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<UserPageVm>(() => BlobQueryManager.GetUserPageVmAsync(id, page.Value, pageSize.Value));

            ViewBag.CustomerId = id;

            var c = Lambda<Func<int, string>>.Cast;
            var pageUrl = c(p => Url.Action("PageForCustomer", "User", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        // GET: /user/single/{id}
        public async Task<ActionResult> Single(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetUserSingleVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}
