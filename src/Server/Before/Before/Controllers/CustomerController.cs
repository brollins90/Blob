using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Contracts.Blob;
using Blob.Contracts.Models;
using Blob.Contracts.ViewModels;

namespace Before.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        public CustomerController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }

        // GET: customer/single/{id}
        public async Task<ActionResult> Single(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CustomerSingleVm cvm = await BlobQueryManager.GetCustomerSingleVmAsync(id.Value).ConfigureAwait(true);
            if (cvm == null)
            {
                return HttpNotFound();
            }

            return View(cvm);
        }

        // GET: customer/edit/{id}
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CustomerSingleVm csvm = await BlobQueryManager.GetCustomerSingleVmAsync(id.Value).ConfigureAwait(true);
            if (csvm == null)
            {
                return HttpNotFound();
            }

            UpdateCustomerDto dto = new UpdateCustomerDto {CustomerId = csvm.CustomerId, Name = csvm.Name};

            return View(dto);
        }

        // POST: customer/edit/{customer}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateCustomerDto dto)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateCustomerAsync(dto).ConfigureAwait(true);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
