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
    public class PerformanceController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }

        public PerformanceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
        }


        [BeforeAuthorize(Operation = "delete", Resource = "performance")]
        public async Task<ActionResult> Delete(long id)
        {
            var viewModel = await BlobQueryManager.GetPerformanceRecordDeleteViewModelAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteModal", viewModel);
        }

        [BeforeAuthorize(Operation = "delete", Resource = "performance")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PerformanceRecordDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DeletePerformanceRecordAsync(model.ToRequest()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DeleteModal", model);
        }

        [BeforeAuthorize(Operation = "page", Resource = "performance")]
        public ActionResult PageForDevice(Guid id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<PerformanceRecordPageViewModel>(() => BlobQueryManager.GetPerformanceRecordPageViewModelAsync(id, page.Value, pageSize.Value));

            ViewBag.DeviceId = id;

            var c = Lambda<Func<int, string>>.Cast;
            var pageUrl = c(p => Url.Action("PageForDevice", "Performance", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        [BeforeAuthorize(Operation = "page", Resource = "performance")]
        public ActionResult PageForStatus(long id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<PerformanceRecordPageViewModel>(() => BlobQueryManager.GetPerformanceRecordPageViewModelForStatusAsync(id, page.Value, pageSize.Value));

            ViewBag.DeviceId = id;

            var c = Lambda<Func<int, string>>.Cast;
            var pageUrl = c(p => Url.Action("PageForDevice", "Performance", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        [BeforeAuthorize(Operation = "single", Resource = "performance")]
        public async Task<ActionResult> Single(long id)
        {
            var viewModel = await BlobQueryManager.GetPerformanceRecordSingleViewModelAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}