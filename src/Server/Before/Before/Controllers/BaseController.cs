using System.Web.Mvc;
using Blob.Managers.Blob;

namespace Before.Controllers
{
    public class BaseController : Controller
    {
        protected IBlobCommandManager BlobCommandManager { get; set; }

        public BaseController(IBlobCommandManager commandManager)
        {
            BlobCommandManager = commandManager;
        }
    }
}