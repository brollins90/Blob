using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        public CustomerController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }
        

        // Disable
        [BeforeAuthorize(Operation = "disable", Resource = "customer")]
        public async Task<ActionResult> Disable(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerDisableVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DisableModal", viewModel);
        }

        [BeforeAuthorize(Operation = "disable", Resource = "customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disable(CustomerDisableVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DisableCustomerAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DisableModal", model);
        }

        // Edit
        [BeforeAuthorize(Operation = "edit", Resource = "customer")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerUpdateVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditModal", viewModel);
        }

        [BeforeAuthorize(Operation = "edit", Resource = "customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateCustomerAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", model);
        }

        // Enable
        [BeforeAuthorize(Operation = "enable", Resource = "customer")]
        public async Task<ActionResult> Enable(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerEnableVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EnableModal", viewModel);
        }

        [BeforeAuthorize(Operation = "enable", Resource = "customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enable(CustomerEnableVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.EnableCustomerAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EnableModal", model);
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
                return RedirectToAction("Index", "Home");
            }
            return View("Register", model);
        }
        
        // Single
        [BeforeAuthorize(Operation = "view", Resource = "customer")]
        public async Task<ActionResult> Single(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerSingleVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}
