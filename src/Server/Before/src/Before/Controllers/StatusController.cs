namespace Before.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Blob.Contracts.ViewModel;
    using Blob.Contracts.ServiceContracts;
    using Filters;
    using Infrastructure.Extensions;

    [Authorize]
    [HandleError]
    public class StatusController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }

        public StatusController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
        }


        [BeforeAuthorize(Operation = "delete", Resource = "status")]
        public async Task<ActionResult> Delete(long id)
        {
            var viewModel = await BlobQueryManager.GetStatusRecordDeleteViewModelAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteModal", viewModel);
        }

        [BeforeAuthorize(Operation = "delete", Resource = "status")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(StatusRecordDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DeleteStatusRecordAsync(model.ToRequest()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DeleteModal", model);
        }

        [BeforeAuthorize(Operation = "page", Resource = "status")]
        public ActionResult MonitorList(Guid id)
        {
            // add paging?
            var pageVm = AsyncHelpers.RunSync<MonitorListViewModel>(() => BlobQueryManager.GetMonitorListViewModelAsync(id));

            ViewBag.DeviceId = id;
            return PartialView("_MonitorList", pageVm);
        }

        [BeforeAuthorize(Operation = "page", Resource = "status")]
        public ActionResult PageForDevice(Guid id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<StatusRecordPageViewModel>(() => BlobQueryManager.GetStatusRecordPageViewModelAsync(id, page.Value, pageSize.Value));

            ViewBag.DeviceId = id;

            var c = Lambda<Func<int, string>>.Cast;
            var pageUrl = c(p => Url.Action("PageForDevice", "Status", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        [BeforeAuthorize(Operation = "single", Resource = "status")]
        public async Task<ActionResult> Single(long id)
        {
            var viewModel = await BlobQueryManager.GetStatusRecordSingleViewModelAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}