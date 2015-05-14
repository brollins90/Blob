using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class PerformanceController : BaseController
    {
        public PerformanceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }


        // GET: /performance/delete/{id}
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetPerformanceRecordDeleteVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", viewModel);
        }

        // POST: /performance/delete/{model}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PerformanceRecordDeleteVm model)
        {
            if (ModelState.IsValid)
            {
                await BlobCommandManager.DeletePerformanceRecordAsync(model.ToDto()).ConfigureAwait(true);
                return Json(new { success = true });
            }
            return PartialView("_Delete", model);
        }

        // GET: /performance/single/{id}
        public async Task<ActionResult> Single(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = await BlobQueryManager.GetPerformanceRecordSingleVmAsync(id.Value).ConfigureAwait(true);
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // GET: /performance/list/{models}
        public ActionResult List(IEnumerable<PerformanceRecordListItemVm> models)
        {
            return PartialView("_List", models);
        }
    }
}
