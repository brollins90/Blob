using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Before.Infrastructure.Identity;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    public class RoleController : BaseController
    {
        public RoleController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager, BeforeUserManagerLocal userManager)
            : base(blobCommandManager, blobQueryManager)
        {
            UserManager = userManager;
        }

        protected BeforeUserManager UserManager { get; set; }


        [BeforeAuthorize(Operation = "view", Resource = "role")]
        public async Task<ActionResult> Index()
        {
            var rolesList = await UserManager.GetAllRolesAsync().ConfigureAwait(true);
            return View(rolesList);
        }


        [BeforeAuthorize(Operation = "create", Resource = "role")]
        public ActionResult Create()
        {
            return PartialView("_CreateModal");
        }


        [BeforeAuthorize(Operation = "create", Resource = "role")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleCreateVm model)
        {
            if (ModelState.IsValid)
            {
                await UserManager.CreateRoleAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new {success = true});
            }
            return PartialView("_CreateModal", model);
        }


        [BeforeAuthorize(Operation = "delete", Resource = "role")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await UserManager.GetRoleDeleteVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteModal", viewModel);
        }

        [BeforeAuthorize(Operation = "delete", Resource = "role")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleDeleteVm model)
        {
            if (ModelState.IsValid)
            {
                await UserManager.DeleteRoleAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DeleteModal", model);
        }


        [BeforeAuthorize(Operation = "edit", Resource = "role")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await UserManager.GetRoleUpdateVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditModal", viewModel);
        }

        [BeforeAuthorize(Operation = "edit", Resource = "role")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                await UserManager.UpdateRoleAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", model);
        }
    }
}