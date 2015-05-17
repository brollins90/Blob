using System;
using System.Net;
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
    public class DashboardController : BaseController
    {
        public DashboardController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }


        // GET: 
        [BeforeAuthorize(Operation = "view", Resource = "customer")]
        public ActionResult DevicesLarge()
        {
            var id = ClaimsPrincipal.Current.Identity.GetCustomerId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DashDevicesLargeVm viewModel = BlobQueryManager.GetDashDevicesLargeVmAsync(id).Result;
            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return PartialView("_DashDevicesLarge", viewModel);
        }
    }
}