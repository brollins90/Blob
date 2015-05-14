using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class StatusController : BaseController
    {
        public StatusController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }

        
        // GET: /status/delete/{id}
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetStatusRecordDeleteVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", viewModel);
        }

        // POST: /status/delete/{model}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(StatusRecordDeleteVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DeleteStatusRecordAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_Delete", model);
        }

        // GET: /status/single/{id}
        public async Task<ActionResult> Single(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetStatusRecordSingleVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // GET: /status/list/{models}
        public ActionResult List(IEnumerable<StatusRecordListItemVm> models)
        {
            return PartialView("_List", models);
        }
    }
}
