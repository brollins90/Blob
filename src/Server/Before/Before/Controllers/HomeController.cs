using System.Web.Mvc;

namespace Before.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: /home/index/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /home/about/
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // GET: /home/contact/
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}