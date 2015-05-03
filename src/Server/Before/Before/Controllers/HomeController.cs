using System.Web.Mvc;

namespace Before.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: /home/index/
        public ActionResult Index()
        {
            return View();
        }
    }
}