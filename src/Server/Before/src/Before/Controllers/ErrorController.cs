namespace Before.Controllers
{
    using System.Web.Mvc;

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