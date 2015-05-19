using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class DeviceController : BaseController
    {
        public DeviceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }

        
        // GET: /device/disable/{id}
        public async Task<ActionResult> Disable(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetDeviceDisableVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DisableModal", viewModel);
        }

        // POST: /device/disable/{model}
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

        // GET: device/edit/{id}
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetDeviceUpdateVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.DeviceTypeId = new SelectList(viewModel.AvailableTypes, "DeviceTypeId", "Value", viewModel.DeviceTypeId);
            return PartialView("_EditModal", viewModel);
        }

        // POST: device/edit/{dto}
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

        // GET: /device/enable/{id}
        public async Task<ActionResult> Enable(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetDeviceEnableVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EnableModal", viewModel);
        }

        // POST: /device/enable/{dto}
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

        // GET: /device/issuecommand/{id}
        public ActionResult IssueCommand(Guid? id, string commandType)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeviceCommandIssueVm viewModel = BlobQueryManager.GetDeviceCommandIssueVm(id.Value, commandType);//.ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return PartialView("_IssueCommandModal", viewModel);
        }

        // POST: /device/issuecommand/{dto}
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

        // GET: /device/PageForCustomer/{customerId}
        public ActionResult PageForCustomer(Guid id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<DevicePageVm>(() => BlobQueryManager.GetDevicePageVmAsync(id, page.Value, pageSize.Value));

            ViewBag.CustomerId = id;

            var c = Lambda<Func<int, string>>.Cast;
            var pageUrl = c(p => Url.Action("PageForCustomer", "Device", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        // GET: /device/single/{id}
        public async Task<ActionResult> Single(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetDeviceSingleVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}
