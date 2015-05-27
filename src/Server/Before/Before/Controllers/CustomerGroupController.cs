using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models;
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

        //
        // GET: /CustomerGroup/Create
        [BeforeAuthorize(Operation = "create", Resource = "group")]
        public async Task<ActionResult> Create()
        {
            ClaimsPrincipal me = ClaimsPrincipal.Current;
            Guid customerId = me.Identity.GetCustomerId();

            var roles = await BlobQueryManager.GetCustomerRolesAsync(customerId);

            CustomerGroupCreateVm viewModel = new CustomerGroupCreateVm
                                              {
                                                  CustomerId = customerId,
                                                  GroupId = Guid.NewGuid(),
                                                  AvailableRoles = roles
                                              };
            return PartialView("_CreateModal", viewModel);
        }

        //
        // POST: /CustomerGroup/Create
        [BeforeAuthorize(Operation = "create", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerGroupCreateVm model, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var result = await BlobCommandManager.CreateCustomerGroupAsync(model.ToDto()).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    selectedRoles = selectedRoles ?? new string[] { };

                    foreach (var role in selectedRoles)
                    {
                        Guid roleId = Guid.Parse(role);
                        await BlobCommandManager.AddRoleToCustomerGroupAsync(new AddRoleToCustomerGroupDto {GroupId = model.GroupId,RoleId = roleId});
                    }
                }
                return Json(new {success = true});
            }
            // Otherwise, start over:
            model.AvailableRoles = await BlobQueryManager.GetCustomerRolesAsync(model.CustomerId);
            return PartialView("_CreateModal", model);
        }

        //
        // GET: /CustomerGroup/Delete
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

        //
        // POST: /CustomerGroup/Delete
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

        //
        // GET: /CustomerGroup/PageForCustomer
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

        //
        // GET: /CustomerGroup/Single
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

        //
        // GET: /CustomerGroup/Update
        [BeforeAuthorize(Operation = "update", Resource = "group")]
        public async Task<ActionResult> Update(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerGroupUpdateVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            var roles = await BlobQueryManager.GetCustomerRolesAsync(viewModel.CustomerId);
            viewModel.AvailableRoles = roles;

            return PartialView("_UpdateModal", viewModel);
        }

        //
        // POST: /CustomerGroup/Update
        [BeforeAuthorize(Operation = "update", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(CustomerGroupUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.UpdateCustomerGroupAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }

            var roles = await BlobQueryManager.GetCustomerRolesAsync(model.CustomerId);
            model.AvailableRoles = roles;
            return PartialView("_UpdateModal", model);
        }
    }
}