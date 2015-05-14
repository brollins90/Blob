using System.Web.Mvc;
using Blob.Contracts.ServiceContracts;

namespace Before.Controllers
{
    public class BaseController : Controller
    {
        protected IBlobQueryManager BlobQueryManager { get; set; }
        protected IBlobCommandManager BlobCommandManager { get; set; }

        public BaseController(IBlobCommandManager blobCommandManager, IBlobQueryManager blobQueryManager)
        {
            BlobCommandManager = blobCommandManager;
            BlobQueryManager = blobQueryManager;
        }
    }
}