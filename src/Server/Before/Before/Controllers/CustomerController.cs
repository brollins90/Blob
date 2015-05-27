using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }

        public CustomerController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
        }

        //
        // GET: /Customer/Disable
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

        //
        // POST: /Customer/Disable
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

        //
        // GET: /Customer/Enable
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

        //
        // POST: /Customer/Enable
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

        //
        // GET: /Customer/Single
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

        //
        // GET: /Customer/Update
        [BeforeAuthorize(Operation = "update", Resource = "customer")]
        public async Task<ActionResult> Update(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerUpdateVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_UpdateModal", viewModel);
        }

        //
        // POST: /Customer/Update
        [BeforeAuthorize(Operation = "update", Resource = "customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(CustomerUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateCustomerAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_UpdateModal", model);
        }
    }
}
