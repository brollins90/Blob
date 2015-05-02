using System.Web.Mvc;

namespace Before.Controllers
{
    [Authorize]
    [RoutePrefix("home")]
    [Route("{action=index}")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Route]
        [Route("~/")]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("about")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        [Route("contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}