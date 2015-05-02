using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Infrastructure.Extensions;
using Before.ViewModels;
using Blob.Core.Domain;
using Blob.Managers.Blob;
using Microsoft.AspNet.Identity;

namespace Before.Controllers
{
    [Authorize]
    //[RoutePrefix("customer")]
    //[Route("{action=index}")]
    public class CustomerController : BaseController
    {
        public CustomerController(IBlobCommandManager manager) : base(manager) { }

        //
        // GET: customer
        //[Route("index")]
        public ActionResult Index()
        {
            var customerId = User.Identity.GetCustomerId();
            return RedirectToAction("Details", new { id = customerId } );
        }

        //
        // GET: customer/details/{id}
        //[Route("details/{id}")]
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = await BlobCommandManager.GetCustomerByIdAsync(id.Value).ConfigureAwait(true);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel cvm = new CustomerViewModel(customer);

            return View(cvm);
        }

        //
        // GET: customer/edit/{id}
        //[Route("edit/{id}")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = await BlobCommandManager.GetCustomerByIdAsync(id.Value).ConfigureAwait(true);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel cvm = new CustomerViewModel(customer);

            return View(cvm);
        }

        //
        // POST: customer/edit/{customer}
        [HttpPost]
        //[Route("edit/{customer}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customer customerData = await BlobCommandManager.GetCustomerByIdAsync(customer.Id).ConfigureAwait(true);

                    // update the fields that we will allow to change
                    customerData.Name = customer.Name;
                    await BlobCommandManager.UpdateCustomerAsync(customerData);

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View();
        }
    }
}
