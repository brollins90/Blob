using System.Security.Claims;
using System.Web.Mvc;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    //[Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
            : base(blobCommandManager, blobQueryManager) { }

    
        [AllowAnonymous]
        public ActionResult Index()
        {
            var c = ClaimsPrincipal.Current;
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }
    }
}
