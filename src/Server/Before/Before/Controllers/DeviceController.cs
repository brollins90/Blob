using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Core.Domain;
using Blob.Managers.Blob;
using log4net;

namespace Before.Controllers
{
    [Authorize]
    [RoutePrefix("device")]
    [Route("{action=index}")]
    public class DeviceController : Controller
    {
        private ILog _log;
        private readonly IBlobManager _blobManager;

        public DeviceController(BlobManager manager, ILog log)
        {
            _blobManager = manager;
            _log = log;
        }

        [Route("index")]
        public async Task<ActionResult> Index(Guid? selectedCustomer)
        {
            IList<Customer> customers = await _blobManager.GetAllCustomersAsync();
            ViewBag.SelectedCustomer = new SelectList(customers, "Id", "Name", selectedCustomer);
            Guid customerId = selectedCustomer.GetValueOrDefault();

            //Guid customerId = Guid.Parse(User.Identity.GetCustomerId());

            IList<Device> devices;
            if (customerId.Equals(Guid.Parse("00000000-0000-0000-0000-000000000000"))) //Guid.Parse("79720728-171C-48A4-A866-5F905C8FDB9F")
            {
                devices = await _blobManager.GetAllDevicesAsync();
            }
            else
            {
                devices = await _blobManager.FindDevicesForCustomerAsync(customerId);
            }
            return View(devices.ToList());
        }

        [Route("details")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = await _blobManager.GetDeviceByIdAsync(id.Value);
            if (device == null)
            {
                return HttpNotFound();
            }

            await PopulateDeviceTypeDropDownList(device.DeviceType.Value);
            return View(device);
        }

        [Route("edit")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = await _blobManager.GetDeviceByIdAsync(id.Value);
            if (device == null)
            {
                return HttpNotFound();
            }

            await PopulateDeviceTypeDropDownList(device.DeviceType.Value);
            return View(device);
        }

        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid? id, FormCollection collection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = await _blobManager.GetDeviceByIdAsync(id.Value);
            if (device == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                if (TryUpdateModel(device, "", new[] { "DeviceName", "DeviceTypeId" }))
                {
                    try
                    {
                        device.DeviceName = collection["deviceName"];

                        await _blobManager.UpdateDeviceAsync(device);

                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("Error", e);
                    }
                }
            }

            await PopulateDeviceTypeDropDownList(device.DeviceType.Value);
            return View(device);
        }

        [Route("delete")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = await _blobManager.GetDeviceByIdAsync(id.Value);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        [HttpPost]
        [Route("delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _blobManager.RemoveDeviceByIdAsync(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }

        private async Task PopulateDeviceTypeDropDownList(object selectedDeviceType = null)
        {
            var deviceTypes = await _blobManager.GetAllDeviceTypesAsync();
            ViewBag.DeviceType = new SelectList(deviceTypes, "DeviceType", "Value", selectedDeviceType);
        }
    }
}
