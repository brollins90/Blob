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
    public class DeviceController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }

        public DeviceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
        }


        // Disable
        [BeforeAuthorize(Operation = "disable", Resource = "device")]
        public async Task<ActionResult> Disable(Guid id)
        {
            var viewModel = await BlobQueryManager.GetDeviceDisableVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DisableModal", viewModel);
        }

        [BeforeAuthorize(Operation = "disable", Resource = "device")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disable(DeviceDisableVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DisableDeviceAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DisableModal", model);
        }

        // Edit
        [BeforeAuthorize(Operation = "edit", Resource = "device")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await BlobQueryManager.GetDeviceUpdateVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.DeviceTypeId = new SelectList(viewModel.AvailableTypes, "DeviceTypeId", "Value", viewModel.DeviceTypeId);
            return PartialView("_EditModal", viewModel);
        }

        [BeforeAuthorize(Operation = "edit", Resource = "device")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DeviceUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateDeviceAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", model);
        }

        // Enable
        [BeforeAuthorize(Operation = "enable", Resource = "device")]
        public async Task<ActionResult> Enable(Guid id)
        {
            var viewModel = await BlobQueryManager.GetDeviceEnableVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EnableModal", viewModel);
        }

        [BeforeAuthorize(Operation = "enable", Resource = "device")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enable(DeviceEnableVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.EnableDeviceAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EnableModal", model);
        }

        // IssueCommand
        [BeforeAuthorize(Operation = "issuecommand", Resource = "device")]
        public ActionResult IssueCommand(Guid id, string commandType)
        {
            DeviceCommandIssueVm viewModel = BlobQueryManager.GetDeviceCommandIssueVm(id, commandType);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return PartialView("_IssueCommandModal", viewModel);
        }

        [BeforeAuthorize(Operation = "issuecommand", Resource = "device")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IssueCommand(DeviceCommandIssueVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.IssueCommandAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_IssueCommandModal", model);
        }

        // List
        [BeforeAuthorize(Operation = "view", Resource = "device")]
        public ActionResult List(Guid? id, int? page, int? pageSize)
        {
            ViewBag.SearchId = (id.HasValue) ? id.Value : ClaimsPrincipal.Current.Identity.GetCustomerId();
            ViewBag.PageNum = page;
            ViewBag.PageSize = pageSize;
            return View();
        }

        // Page
        [BeforeAuthorize(Operation = "view", Resource = "device")]
        public ActionResult PageForCustomer(Guid id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<DevicePageVm>(() => BlobQueryManager.GetDevicePageVmAsync(id, page.Value, pageSize.Value));

            ViewBag.CustomerId = id;

            var c = Lambda<Func<int, string>>.Cast;
            Func<int, string> pageUrl = c(p => Url.Action("PageForCustomer", "Device", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        // Single
        [BeforeAuthorize(Operation = "view", Resource = "device")]
        public async Task<ActionResult> Single(Guid id)
        {
            var viewModel = await BlobQueryManager.GetDeviceSingleVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}
