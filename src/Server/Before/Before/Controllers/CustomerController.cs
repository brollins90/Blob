using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Contracts.Blob;
using Blob.Contracts.Dto.ViewModels;

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

            var singleVm = await BlobQueryManager.GetCustomerSingleVmAsync(id.Value).ConfigureAwait(true);
            if (singleVm == null)
            {
                return HttpNotFound();
            }

            return View(singleVm);
        }

        // GET: customer/edit/{id}
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var singleVm = await BlobQueryManager.GetCustomerSingleVmAsync(id.Value).ConfigureAwait(true);
            if (singleVm == null)
            {
                return HttpNotFound();
            }
            return View(new CustomerUpdateVm { CustomerId = singleVm.CustomerId, CustomerName = singleVm.Name });
        }

        // POST: customer/edit/{customer}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerUpdateVm vm)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateCustomerAsync(vm.ToDto()).ConfigureAwait(true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
