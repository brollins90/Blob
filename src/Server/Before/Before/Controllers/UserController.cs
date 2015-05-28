using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Before.Infrastructure.Extensions;
using Before.Infrastructure.Identity;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }
        protected IUserManagerService UserManager { get; set; }
        protected ISignInManager SignInManager { get; set; }

        public UserController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager, IUserManagerService userManager, ISignInManager signInManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }


        [BeforeAuthorize(Operation = "create", Resource = "user")]
        public ActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        [BeforeAuthorize(Operation = "create", Resource = "user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserCreateVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.CreateUserAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EnableModal", model);
        }

        [BeforeAuthorize(Operation = "disable", Resource = "user")]
        public async Task<ActionResult> Disable(Guid id)
        {
            var viewModel = await BlobQueryManager.GetUserDisableVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DisableModal", viewModel);
        }

        [BeforeAuthorize(Operation = "disable", Resource = "user")]
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

        [BeforeAuthorize(Operation = "edit", Resource = "user")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await BlobQueryManager.GetUserUpdateVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScheduleId = new SelectList(viewModel.AvailableSchedules, "ScheduleId", "Name", viewModel.ScheduleId);
            return PartialView("_EditModal", viewModel);
        }

        [BeforeAuthorize(Operation = "edit", Resource = "user")]
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

        [BeforeAuthorize(Operation = "editpassword", Resource = "user")]
        public async Task<ActionResult> EditPassword(Guid id)
        {
            var viewModel = await BlobQueryManager.GetUserUpdatePasswordVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditPasswordModal", viewModel);
        }

        [BeforeAuthorize(Operation = "editpassword", Resource = "user")]
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
                    // todo: add signout and sign back in
                    SignInManager.SignOut();
                    await SignInManager.PasswordSignInAsync(user.UserName, model.NewPassword, isPersistent: false, shouldLockout: false).ConfigureAwait(true);
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

        [BeforeAuthorize(Operation = "enable", Resource = "user")]
        public async Task<ActionResult> Enable(Guid id)
        {
            var viewModel = await BlobQueryManager.GetUserEnableVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EnableModal", viewModel);
        }

        [BeforeAuthorize(Operation = "enable", Resource = "user")]
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

        [BeforeAuthorize(Operation = "page", Resource = "user")]
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

        [BeforeAuthorize(Operation = "single", Resource = "user")]
        public async Task<ActionResult> Single(Guid id)
        {
            var viewModel = await BlobQueryManager.GetUserSingleVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}
