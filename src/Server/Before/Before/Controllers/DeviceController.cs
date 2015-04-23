using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blob.Core.Data;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Managers.Blob;
using log4net;

namespace Before.Controllers
{
    //[Authorize]
    public class DeviceController : Controller
    {
        private ILog _log;
        private IBlobManager _blobManager;

        public DeviceController()
        {

            BlobDbContext _context = new BlobDbContext();
            IAccountRepository _accountRepository = new Blob.Data.Repositories.AccountRepository(_context);
            IStatusRepository _statusRepository = new Blob.Data.Repositories.StatusRepository(_context);

            _blobManager = new BlobManager(_accountRepository, _statusRepository, LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
        }

        // GET: Device
        public async Task<ActionResult> Index(Guid? selectedCustomer)
        {
            var customers = await _blobManager.GetAllCustomersAsync(); //.ToList();
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
                devices = await _blobManager.FindDevicesForCustomer(customerId);
            }
            return View(devices.ToList());
        }

        // GET: Device/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var device = await _blobManager.GetDeviceByIdAsync(id.Value);
            if (device == null)
            {
                return HttpNotFound();
            }
            //var statuses = await _blobManager.GetStatusForDeviceAsync(device.Id)
            //var statuses = device.Statuses.ToList();
            return View(device);
        }

        // GET: Device/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: Device/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Device/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            return View();
        }

        // POST: Device/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid? id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Device/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            return View();
        }

        // POST: Device/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid? id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
