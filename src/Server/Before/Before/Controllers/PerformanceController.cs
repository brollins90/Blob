using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Contracts.Blob;
using Blob.Contracts.Dto.ViewModels;

namespace Before.Controllers
{
    [Authorize]
    public class PerformanceController : BaseController
    {
        public PerformanceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }


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
