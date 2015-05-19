using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        public CustomerController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }

        
        // GET: /customer/disable/{id}
        [BeforeAuthorize(Operation = "disable", Resource = "customer")]
        public async Task<ActionResult> Disable(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetCustomerDisableVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DisableModal", viewModel);
        }

        // POST: /customer/disable/{model}
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

        // GET: /customer/edit/{id}
        [BeforeAuthorize(Operation = "edit", Resource = "customer")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetCustomerUpdateVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditModal", viewModel);
        }

        // POST: /customer/edit/{model}
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

        // GET: /customer/enable/{id}
        [BeforeAuthorize(Operation = "enable", Resource = "customer")]
        public async Task<ActionResult> Enable(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetCustomerEnableVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EnableModal", viewModel);
        }

        // POST: /customer/enable/{model}
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
        
        // GET: /customer/single/{id}
        [BeforeAuthorize(Operation = "view", Resource = "customer")]
        public async Task<ActionResult> Single(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetCustomerSingleVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}
