//using System;
//using System.IO;
//using System.Web;
//using System.Web.Mvc;

//namespace Before.Controllers
//{
//    [Authorize]
//    public class FileUploadController : Controller
//    {
//        //public ActionResult UploadFile()
//        //{
//        //    return View("_UploadFile");
//        //}

//        //[HttpPost]
//        ////[ValidateAntiForgeryToken]
//        //public ActionResult UploadFile(FormCollection formCollection)
//        //{
//        //    if (Request != null)
//        //    {
//        //        HttpPostedFileBase file = Request.Files["fileField"];
//        //        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
//        //        {
//        //            string fileName = file.FileName;
//        //            //string tempFileName = Guid.NewGuid().ToString();
//        //            string fileContentType = file.ContentType;
//        //            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
//        //            file.SaveAs(path);
//        //            return Json(new {success = true});
//        //            //byte[] fileBytes = new byte[file.ContentLength];
//        //            //file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
//        //        }
//        //    }
//        //    return PartialView("_UploadFile");
//        //}

//        public ActionResult UploadFile()
//        {
//            return PartialView("_UploadFile");
//        }

//        [HttpPost]
//        public ActionResult UploadFile(FormCollection formCollection)
//        {
//            if (Request != null)
//            {
//                HttpPostedFileBase file = Request.Files["fileField"];
//                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
//                {
//                    string fileName = file.FileName;
//                    string fileContentType = file.ContentType;
//                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
//                    file.SaveAs(path);
//                    //byte[] fileBytes = new byte[file.ContentLength];
//                    //file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
//                }
//            }
//            return PartialView("_UploadFile");
//        }
//    }
//}