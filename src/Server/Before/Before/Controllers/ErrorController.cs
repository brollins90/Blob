using System.Web.Mvc;

namespace Before.Controllers
{
    [Authorize]
    [HandleError]
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public ActionResult ModalError()
        {
            return PartialView("_ErrorModal");
        }
    }
}
