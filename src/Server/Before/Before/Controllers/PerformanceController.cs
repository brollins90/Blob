using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blob.Contracts.Blob;
using Blob.Contracts.ViewModels;

namespace Before.Controllers
{
    [Authorize]
    public class PerformanceController : BaseController
    {
        public PerformanceController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }

        // GET: performance/single/{id}
        public async Task<ActionResult> Single(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PerformanceRecordSingleVm singleVm = await BlobQueryManager.GetPerformanceRecordSingleVmAsync(id.Value).ConfigureAwait(true);
            if (singleVm == null)
            {
                return HttpNotFound();
            }

            return View(singleVm);
        }
    }
}
