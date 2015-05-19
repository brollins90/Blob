using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using PagedList;

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
            return PartialView("_Disable", viewModel);
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
            return PartialView("_Disable", model);
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
            return PartialView("_Edit", viewModel);
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
            return PartialView("_Edit", model);
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
            return PartialView("_Enable", viewModel);
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
            return PartialView("_Enable", model);
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

            return PartialView("_IssueCommand", viewModel);
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
            return PartialView("_IssueCommand", model);
        }

        // GET: /device/list/{models}
        public ActionResult List(IEnumerable<DeviceListItemVm> models)
        {
            return PartialView("_List", models);
        }

        // GET: /device/ListFromCustomer/{id}
        public ActionResult PageForCustomer(Guid customerId, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            DevicePageVm pageVm = AsyncHelpers.RunSync<DevicePageVm>(() => BlobQueryManager.GetDevicePageVmAsync(customerId, page.Value, pageSize.Value));

            ViewBag.CustomerId = customerId;
            // Func<int, string>
            var c = Lambda<Func<int, string>>.Cast;
            var pageUrl = c(p => Url.Action("PageFromCustomer", "Device", routeValues: new {id = customerId, p, pageSize = pageVm.PageSize}));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();// new StaticPagedList<DeviceListItemVm>(models.Items, models.PageNum, models.PageSize, models.TotalCount).GetMetaData();
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
