using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Before.Filters;
using Before.Infrastructure.Identity;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager, BeforeUserManagerLocal userManager)
            : base(blobCommandManager, blobQueryManager)
        {
            UserManager = userManager;
        }

        protected BeforeUserManager UserManager { get; set; }


        [BeforeAuthorize(Operation = "view", Resource = "group")]
        public async Task<ActionResult> Index()
        {
            var groupList = await UserManager.GetAllGroupsAsync().ConfigureAwait(true);
            return View(groupList);
        }


        [BeforeAuthorize(Operation = "create", Resource = "group")]
        public ActionResult Create()
        {
            return PartialView("_CreateModal");
        }


        [BeforeAuthorize(Operation = "create", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupCreateVm model)
        {
            if (ModelState.IsValid)
            {
                await UserManager.CreateGroupAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new {success = true});
            }
            return PartialView("_CreateModal", model);
        }


        [BeforeAuthorize(Operation = "delete", Resource = "group")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await UserManager.GetGroupDeleteVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteModal", viewModel);
        }

        [BeforeAuthorize(Operation = "delete", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GroupDeleteVm model)
        {
            if (ModelState.IsValid)
            {
                await UserManager.DeleteGroupAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_DeleteModal", model);
        }


        [BeforeAuthorize(Operation = "edit", Resource = "group")]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await UserManager.GetGroupUpdateVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EditModal", viewModel);
        }

        [BeforeAuthorize(Operation = "edit", Resource = "group")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GroupUpdateVm model)
        {
            if (ModelState.IsValid)
            {
                await UserManager.UpdateGroupAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", model);
        }
    }
}