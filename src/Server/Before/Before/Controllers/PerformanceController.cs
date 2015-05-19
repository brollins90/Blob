using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class PerformanceController : BaseController
    {
        public PerformanceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }


        // GET: /performance/delete/{id}
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetPerformanceRecordDeleteVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteModal", viewModel);
        }

        // POST: /performance/delete/{model}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PerformanceRecordDeleteVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DeletePerformanceRecordAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DeleteModal", model);
        }

        // GET: /performance/PageForCustomer/{deviceId}
        public ActionResult PageForDevice(Guid id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<PerformanceRecordPageVm>(() => BlobQueryManager.GetPerformanceRecordPageVmAsync(id, page.Value, pageSize.Value));

            ViewBag.DeviceId = id;

            var c = Lambda<Func<int, string>>.Cast;
            var pageUrl = c(p => Url.Action("PageForDevice", "Performance", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        // GET: /performance/PageForCustomer/{deviceId}
        public ActionResult PageForStatus(long id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<PerformanceRecordPageVm>(() => BlobQueryManager.GetPerformanceRecordPageVmForStatusAsync(id, page.Value, pageSize.Value));

            ViewBag.DeviceId = id;

            var c = Lambda<Func<int, string>>.Cast;
            var pageUrl = c(p => Url.Action("PageForDevice", "Performance", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        // GET: /performance/single/{id}
        public async Task<ActionResult> Single(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetPerformanceRecordSingleVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}
