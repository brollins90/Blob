using System.Web.Mvc;
using Before.Filters;

namespace Before.Controllers
{
    [Authorize]
    //[BeforeAuthorize(Operation = "disable", Resource = "customergroups")]
    public class CustomerGroupsController : Controller
    {
        // GET: CustomerGroups
        public ActionResult Index()
        {
            return View();
        }
    }
}