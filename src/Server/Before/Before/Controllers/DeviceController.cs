using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Contracts.Blob;
using Blob.Contracts.Models;
using Blob.Contracts.ViewModels;

//using Blob.Core.Domain;

namespace Before.Controllers
{
    [Authorize]
    public class DeviceController : BaseController
    {
        public DeviceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }

        // GET: device/single/{id}
        public async Task<ActionResult> Single(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeviceSingleVm singleVm = await BlobQueryManager.GetDeviceSingleVmAsync(id.Value).ConfigureAwait(true);
            if (singleVm == null)
            {
                return HttpNotFound();
            }

            return View(singleVm);
        }

        // GET: device/edit/{id}
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeviceUpdateVm updateVm = await BlobQueryManager.GetDeviceUpdateVmAsync(id.Value).ConfigureAwait(true);
            if (updateVm == null)
            {
                return HttpNotFound();
            }

            ViewBag.DeviceTypeId = new SelectList(updateVm.AvailableTypes, "DeviceTypeId", "Value", updateVm.DeviceTypeId);

            return View(updateVm);
        }

        // POST: device/edit/{dto}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DeviceUpdateVm updateVm)
        {
            if (ModelState.IsValid)
            {
                UpdateDeviceDto dto = new UpdateDeviceDto { DeviceId = updateVm.DeviceId, DeviceTypeId = updateVm.DeviceTypeId, Name = updateVm.Name };
            
                await BlobCommandManager.UpdateDeviceAsync(dto).ConfigureAwait(true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: /device/disable/{id}
        public async Task<ActionResult> Disable(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeviceSingleVm singleVm = await BlobQueryManager.GetDeviceSingleVmAsync(id.Value).ConfigureAwait(true);
            if (singleVm == null)
            {
                return HttpNotFound();
            }

            DisableDeviceDto dto = new DisableDeviceDto { DeviceId = singleVm.DeviceId };
            return View(dto);
        }

        // POST: /device/disableconfirmed/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableConfirmed(DisableDeviceDto id)
        {
            await BlobCommandManager.DisableDeviceAsync(id);
            return RedirectToAction("Index");
        }
    }
}
