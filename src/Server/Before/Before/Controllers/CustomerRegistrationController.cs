﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Before.Infrastructure.Identity;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    [HandleError]
    public class CustomerRegistrationController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }
        protected ISignInManager SignInManager { get; set; }

        public CustomerRegistrationController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager, ISignInManager signInManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
            SignInManager = signInManager;
        }


        // Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var viewModel = new CustomerRegisterVm();
            return View("Register", viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(CustomerRegisterVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.RegisterCustomerAsync(model.ToDto()).ConfigureAwait(true);

                // login hack?

                SignInStatusDto resultDto = await SignInManager.PasswordSignInAsync(model.UserRegistration.UserName, model.UserRegistration.Password, isPersistent: false, shouldLockout: false);
                //switch (resultDto.ToResult())
                //{
                //    case SignInStatus.Success:
                //        return RedirectToAction("Index", "Home");
                //    case SignInStatus.Failure:
                //    default:
                //        ModelState.AddModelError("", "Invalid login attempt.");
                //return RedirectToAction("Index", "Home");
                //}
                //AddErrors(result);
                return RedirectToAction("Index", "Home");
            }
            return View("Register", model);
        }
    }
}
