using System;
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


        // GET: device/single/{id}
        public async Task<ActionResult> Single(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var singleVm = await BlobQueryManager.GetDeviceSingleVmAsync(id.Value).ConfigureAwait(true);
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

            var updateVm = await BlobQueryManager.GetDeviceUpdateVmAsync(id.Value).ConfigureAwait(true);
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
        public async Task<ActionResult> Edit(DeviceUpdateVm vm)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateDeviceAsync(vm.ToDto()).ConfigureAwait(true);
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

            var singleVm = await BlobQueryManager.GetDeviceSingleVmAsync(id.Value).ConfigureAwait(true);
            if (singleVm == null)
            {
                return HttpNotFound();
            }

            return View(new DeviceDisableVm { DeviceId = singleVm.DeviceId, Name = singleVm.DeviceName });
        }

        // POST: /device/disable/{dto}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disable(DeviceDisableVm vm)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DisableDeviceAsync(vm.ToDto()).ConfigureAwait(true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: /device/enable/{id}
        public async Task<ActionResult> Enable(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var singleVm = await BlobQueryManager.GetDeviceSingleVmAsync(id.Value).ConfigureAwait(true);
            if (singleVm == null)
            {
                return HttpNotFound();
            }

            return View(new DeviceEnableVm { DeviceId = singleVm.DeviceId, Name = singleVm.DeviceName });
        }

        // POST: /device/enable/{dto}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enable(DeviceEnableVm vm)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.EnableDeviceAsync(vm.ToDto()).ConfigureAwait(true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
