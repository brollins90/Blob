using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class CustomerGroupController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }

        public CustomerGroupController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
        }


        [BeforeAuthorize(Operation = "create", Resource = "group")]
        public async Task<ActionResult> Create()
        {
            ClaimsPrincipal me = ClaimsPrincipal.Current;
            Guid customerId = me.Identity.GetCustomerId();

            //Get a SelectList of Roles to choose from in the View:
            var roles = await BlobQueryManager.GetCustomerRolesAsync(customerId);
            ViewBag.RolesList = new SelectList(roles.ToList(), "RoleId", "Name");

            CustomerGroupCreateVm viewModel = new CustomerGroupCreateVm
                                              {
                                                  CustomerId = customerId,
                                                  GroupId = Guid.NewGuid()
                                              };
            return PartialView("_CreateModal", viewModel);
        }


        [BeforeAuthorize(Operation = "create", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerGroupCreateVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.CreateCustomerGroupAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new {success = true});
            }
            return PartialView("_CreateModal", model);
        }


        [BeforeAuthorize(Operation = "delete", Resource = "group")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerGroupDeleteVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteModal", viewModel);
        }

        [BeforeAuthorize(Operation = "delete", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CustomerGroupDeleteVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DeleteCustomerGroupAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new {success = true});
            }
            return PartialView("_DeleteModal", model);
        }


        [BeforeAuthorize(Operation = "edit", Resource = "group")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerGroupUpdateVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditModal", viewModel);
        }

        [BeforeAuthorize(Operation = "edit", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerGroupUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateCustomerGroupAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new {success = true});
            }
            return PartialView("_EditModal", model);
        }

        // Page
        [BeforeAuthorize(Operation = "view", Resource = "group")]
        public ActionResult PageForCustomer(Guid id, int? page, int? pageSize)
        {
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<CustomerGroupPageVm>(() => BlobQueryManager.GetCustomerGroupPageVmAsync(id, page.Value, pageSize.Value));

            ViewBag.CustomerId = id;

            var c = Lambda<Func<int, string>>.Cast;
            Func<int, string> pageUrl = c(p => Url.Action("PageForCustomer", "CustomerGroup", routeValues: new { id = id, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();
            return PartialView("_Page", pageVm);
        }

        [BeforeAuthorize(Operation = "view", Resource = "group")]
        public async Task<ActionResult> Single(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerGroupSingleVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
    }
}