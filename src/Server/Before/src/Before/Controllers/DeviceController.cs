namespace Before.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Blob.Contracts.ViewModel;
    using Blob.Contracts.ServiceContracts;
    using Filters;
    using Infrastructure.Extensions;

    [Authorize]
    [HandleError]
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
            var viewModel = await BlobQueryManager.GetDeviceDisableViewModelAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DisableModal", viewModel);
        }

        [BeforeAuthorize(Operation = "disable", Resource = "device")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disable(DeviceDisableViewModel model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DisableDeviceAsync(model.ToRequest()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DisableModal", model);
        }

        // Edit
        [BeforeAuthorize(Operation = "edit", Resource = "device")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await BlobQueryManager.GetDeviceUpdateViewModelAsync(id).ConfigureAwait(true);
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
        public async Task<ActionResult> Edit(DeviceUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateDeviceAsync(model.ToRequest()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", model);
        }

        // Enable
        [BeforeAuthorize(Operation = "enable", Resource = "device")]
        public async Task<ActionResult> Enable(Guid id)
        {
            var viewModel = await BlobQueryManager.GetDeviceEnableViewModelAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EnableModal", viewModel);
        }

        [BeforeAuthorize(Operation = "enable", Resource = "device")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enable(DeviceEnableViewModel model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.EnableDeviceAsync(model.ToRequest()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EnableModal", model);
        }

        // IssueCommand
        [BeforeAuthorize(Operation = "issuecommand", Resource = "device")]
        public ActionResult IssueCommand(Guid id, string commandType)
        {
            DeviceCommandIssueViewModel viewModel = BlobQueryManager.GetDeviceCommandIssueViewModel(id, commandType);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return PartialView("_IssueCommandModal", viewModel);
        }

        [BeforeAuthorize(Operation = "issuecommand", Resource = "device")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IssueCommand(DeviceCommandIssueViewModel model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.IssueCommandAsync(model.ToRequest()).ConfigureAwait(true);
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

            var pageVm = AsyncHelpers.RunSync<DevicePageViewModel>(() => BlobQueryManager.GetDevicePageViewModelAsync(id, page.Value, pageSize.Value));

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
            var viewModel = await BlobQueryManager.GetDeviceSingleViewModelAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}