﻿using System;
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
        public UserController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager, BeforeUserManager userManager, BeforeSignInManager signInManager)
            : base(blobCommandManager, blobQueryManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        protected BeforeUserManager UserManager { get; set; }
        protected BeforeSignInManager SignInManager { get; set; }


        //// GET: /user/create
        //[AllowAnonymous]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /user/create
        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(UserCreateVm model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        BeforeUser user = new BeforeUser { UserName = model.UserName };
        //        IdentityResultDto result = await UserManager.CreateAsync(user.ToDto(), model.Password);
        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user.ToDto(), isPersistent: false, rememberBrowser: false);

        //            //For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            //Send an email with this link
        //            //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

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
            return PartialView("_Disable", viewModel);
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
            return PartialView("_Disable", model);
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
            return PartialView("_Edit", viewModel);
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
            return PartialView("_Edit", model);
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
            return PartialView("_EditPassword", viewModel);
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
            return PartialView("_EditPassword", model);
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
            return PartialView("_Enable", viewModel);
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
            return PartialView("_Enable", model);
        }

        // GET: /user/list/{models}
        public ActionResult List(IEnumerable<UserListItemVm> models)
        {
            return PartialView("_List", models);
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
