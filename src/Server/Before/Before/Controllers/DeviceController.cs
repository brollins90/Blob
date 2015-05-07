using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Contracts.Blob;
using Blob.Contracts.Dto.ViewModels;

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
        public ActionResult IssueCommand(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeviceCommandIssueVm viewModel = BlobQueryManager.GetDeviceCommandIssueVm(id.Value);//.ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            // temp default 
            viewModel.CommandData = @"dir >> c:\_\fromACommand.txt";
            return PartialView("_IssueCommand", viewModel);
        }

        // POST: /device/issuecommand/{dto}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IssueCommand(DeviceCommandIssueVm model)
        {
            DeviceCommandIssueVm viewModel2 = BlobQueryManager.GetDeviceCommandIssueVm(model.DeviceId);//.ConfigureAwait(true);
            if (ModelState.IsValid)
            {
                //model.SelectedCommand = viewModel2.AvailableCommands.Select(x=>x.CommandType = )
                await BlobCommandManager.IssueCommandAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            model.AvailableCommands = viewModel2.AvailableCommands;
            return PartialView("_IssueCommand", model);
        }

        // GET: /device/list/{models}
        public ActionResult List(IEnumerable<DeviceListItemVm> models)
        {
            return PartialView("_List", models);
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
