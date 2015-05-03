//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using Blob.Core.Domain;
//using Blob.Managers.Blob;

//namespace Before.Controllers
//{
//    [Authorize]
//    public class DeviceController : BaseController
//    {
//        public DeviceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
//            : base(blobCommandManager, blobQueryManager) { }

//        //// GET: /device/index/{customerId}
//        //public async Task<ActionResult> Index(Guid? selectedCustomer)
//        //{
//        //    IList<Customer> customers = await BlobCommandManager.GetAllCustomersAsync();
//        //    ViewBag.SelectedCustomer = new SelectList(customers, "Id", "Name", selectedCustomer);
//        //    Guid customerId = selectedCustomer.GetValueOrDefault();

//        //    //Guid customerId = Guid.Parse(User.Identity.GetCustomerId());

//        //    IList<Device> devices;
//        //    if (customerId.Equals(Guid.Parse("00000000-0000-0000-0000-000000000000"))) //Guid.Parse("79720728-171C-48A4-A866-5F905C8FDB9F")
//        //    {
//        //        devices = await BlobCommandManager.GetAllDevicesAsync();
//        //    }
//        //    else
//        //    {
//        //        devices = await BlobCommandManager.FindDevicesForCustomerAsync(customerId);
//        //    }
//        //    return View(devices.ToList());
//        //}

//        // GET: /device/details/{id}
//        public async Task<ActionResult> Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            Device device = await BlobCommandManager.GetDeviceByIdAsync(id.Value);
//            if (device == null)
//            {
//                return HttpNotFound();
//            }

//            await PopulateDeviceTypeDropDownList(device.DeviceType.Value);
//            return View(device);
//        }

//        // GET: /device/edit/{id}
//        public async Task<ActionResult> Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            Device device = await BlobCommandManager.GetDeviceByIdAsync(id.Value);
//            if (device == null)
//            {
//                return HttpNotFound();
//            }

//            await PopulateDeviceTypeDropDownList(device.DeviceType.Value);
//            return View(device);
//        }

//        // POST: /device/edit/{id}
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit(Guid? id, FormCollection collection)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            Device device = await BlobCommandManager.GetDeviceByIdAsync(id.Value);
//            if (device == null)
//            {
//                return HttpNotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                if (TryUpdateModel(device, "", new[] { "DeviceName", "DeviceTypeId" }))
//                {
//                    try
//                    {
//                        device.DeviceName = collection["deviceName"];

//                        await BlobCommandManager.UpdateDeviceAsync(device);

//                        return RedirectToAction("Index");
//                    }
//                    catch (Exception e)
//                    {
//                        ModelState.AddModelError("Error", e);
//                    }
//                }
//            }

//            await PopulateDeviceTypeDropDownList(device.DeviceType.Value);
//            return View(device);
//        }

//        // GET: /device/delete/{id}
//        public async Task<ActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            Device device = await BlobCommandManager.GetDeviceByIdAsync(id.Value);
//            if (device == null)
//            {
//                return HttpNotFound();
//            }
//            return View(device);
//        }

//        // POST: /device/deleteconfirmed/{id}
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(Guid id)
//        {
//            await BlobCommandManager.RemoveDeviceByIdAsync(id);

//            return RedirectToAction("Index");
//        }

//        #region Helpers
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {

//            }
//            base.Dispose(disposing);
//        }

//        private async Task PopulateDeviceTypeDropDownList(object selectedDeviceType = null)
//        {
//            var deviceTypes = await BlobCommandManager.GetAllDeviceTypesAsync();
//            ViewBag.DeviceType = new SelectList(deviceTypes, "DeviceType", "Value", selectedDeviceType);
//        }
//        #endregion
//    }
//}
