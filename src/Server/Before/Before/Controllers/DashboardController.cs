using System;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using Before.Filters;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }

        public DashboardController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
        }


        [BeforeAuthorize(Operation = "view", Resource = "customer")]
        public ActionResult CurrentConnectionsLarge(Guid? id, int? page, int? pageSize)
        {
            Guid searchUsingId = (id.HasValue) ? id.Value : ClaimsPrincipal.Current.Identity.GetCustomerId();
            if (searchUsingId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<DashCurrentConnectionsLargeVm>(() => BlobQueryManager.GetDashCurrentConnectionsLargeVmAsync(searchUsingId, page.Value, pageSize.Value));

            ViewBag.CustomerId = searchUsingId;

            var c = Lambda<Func<int, string>>.Cast;
            Func<int, string> pageUrl = c(p => Url.Action("CurrentConnectionsLarge", "Dashboard", routeValues: new { id = searchUsingId, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();

            return PartialView("_DashCurrentConnectionsLarge", pageVm);
        }


        [BeforeAuthorize(Operation = "view", Resource = "customer")]
        public ActionResult DevicesLarge(Guid? id, int? page, int? pageSize)
        {
            Guid searchUsingId = (id.HasValue) ? id.Value : ClaimsPrincipal.Current.Identity.GetCustomerId();
            if (searchUsingId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;

            var pageVm = AsyncHelpers.RunSync<DashDevicesLargeVm>(() => BlobQueryManager.GetDashDevicesLargeVmAsync(searchUsingId, page.Value, pageSize.Value));

            ViewBag.CustomerId = searchUsingId;

            var c = Lambda<Func<int, string>>.Cast;
            Func<int, string> pageUrl = c(p => Url.Action("DevicesLarge", "Dashboard", routeValues: new { id = searchUsingId, page = p, pageSize = pageVm.PageSize }));
            ViewBag.PageUrl = pageUrl;
            ViewBag.PagingMetaData = pageVm.GetPagedListMetaData();

            return PartialView("_DashDevicesLarge", pageVm);
        }
    }
}