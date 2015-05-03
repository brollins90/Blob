using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models;
using Blob.Contracts.ViewModels;
using Blob.Core.Domain;
using Blob.Managers.Blob;

namespace Before.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        public CustomerController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }

        // GET: customer
        public ActionResult Index()
        {
            var customerId = User.Identity.GetCustomerId();
            return RedirectToAction("Details", new { id = customerId } );
        }

        // GET: customer/details/{id}
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CustomerDetailsVm cdvm = await BlobQueryManager.GetCustomerDetailsViewModelByIdAsync(id.Value).ConfigureAwait(true);
            if (cdvm == null)
            {
                return HttpNotFound();
            }

            return View(cdvm);
        }

        // GET: customer/edit/{id}
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CustomerDetailsVm cdvm = await BlobQueryManager.GetCustomerDetailsViewModelByIdAsync(id.Value).ConfigureAwait(true);
            if (cdvm == null)
            {
                return HttpNotFound();
            }

            return View(cdvm);
        }

        // POST: customer/edit/{customer}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerId, Name")] UpdateCustomerVm updateCustomerVm)
        {
            if (ModelState.IsValid)
            {
                UpdateCustomerDto dto = new UpdateCustomerDto { CustomerId = updateCustomerVm.CustomerId, Name = updateCustomerVm.Name };
                await BlobCommandManager.UpdateCustomerAsync(dto).ConfigureAwait(true);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
