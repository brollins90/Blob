using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Before.Filters;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    [HandleError]
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
            // is there a better way to get the customer id?
            ClaimsPrincipal me = ClaimsPrincipal.Current;
            Guid customerId = me.Identity.GetCustomerId();

            var viewModel = await BlobQueryManager.GetCustomerGroupCreateVmAsync(customerId).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
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
        // GET: /CustomerGroup/Edit
        [BeforeAuthorize(Operation = "edit", Resource = "group")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await BlobQueryManager.GetCustomerGroupUpdateVmAsync(id).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            var allRoles = await BlobQueryManager.GetCustomerRolesAsync(viewModel.CustomerId);
            var groupRoles = await BlobQueryManager.GetCustomerGroupRolesAsync(viewModel.GroupId);

            foreach (var role in allRoles)
            {
                role.Selected = groupRoles.Any(x => x.RoleId == role.RoleId);
            }
            viewModel.AvailableRoles = allRoles;

            return PartialView("_EditModal", viewModel);
        }

        //
        // POST: /CustomerGroup/Edit
        [BeforeAuthorize(Operation = "edit", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerGroupUpdateVm model, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                UpdateCustomerGroupDto dto = model.ToDto();
                dto.RolesIdStrings = selectedRoles ?? new string[] { };
                await BlobCommandManager.UpdateCustomerGroupAsync(dto).ConfigureAwait(true);
                return Json(new { success = true });
            }

            var allRoles = await BlobQueryManager.GetCustomerRolesAsync(model.CustomerId);
            var groupRoles = await BlobQueryManager.GetCustomerGroupRolesAsync(model.GroupId);

            foreach (var role in allRoles)
            {
                role.Selected = groupRoles.Any(x => x.RoleId == role.RoleId);
            }
            return PartialView("_EditModal", model);
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
    }
}