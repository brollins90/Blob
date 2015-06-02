using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Before.Infrastructure.Extensions;
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
        // GET: /Customer/Edit
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

        //
        // POST: /Customer/Edit
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
        
        // List
        [BeforeAuthorize(Operation = "view", Resource = "customer")]
        public ActionResult List(Guid? id, int? page, int? pageSize)
        {
            ViewBag.SearchId = (id.HasValue) ? id.Value : ClaimsPrincipal.Current.Identity.GetCustomerId();
            ViewBag.PageNum = page;
            ViewBag.PageSize = pageSize;
            return View();
        }

        // Page
        [BeforeAuthorize(Operation = "view", Resource = "customer")]
        public ActionResult PageForUser(Guid id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<CustomerPageVm>(() => BlobQueryManager.GetCustomerPageVmAsync(id, page.Value, pageSize.Value));

            ViewBag.CustomerId = id;

            var c = Lambda<Func<int, string>>.Cast;
            Func<int, string> pageUrl = c(p => Url.Action("PageForUser", "Customer", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
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
    }
}
