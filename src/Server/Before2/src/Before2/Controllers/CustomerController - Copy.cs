//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Mvc;

//// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
// http://www.khalidabuhakmeh.com/the-golden-seven-asp-net-mvc-controllers

//namespace Before2.Controllers
//{
//    public class CustomerController : Controller
//    {
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Show()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult New()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Create(EditCustomerModel input)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View("New", input);
//            }
//            var customer = new Customer();
//            // create

//            return RedirectToAction("Show", new { id = customer.Id });
//        }

//        [HttpGet]
//        public IActionResult Edit()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Update(EditCustomerModel input)
//        {
//            // validate and save

//            // maintain state
//            return RedirectToAction(input.ReturnUrl, new { action = "Index" });
//        }

//        [HttpGet]
//        public IActionResult Delete()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Destroy(int id, string returnUrl)
//        {
//            // delete customer


//            return RedirectToAction(returnUrl, new { action = "Index" });
//        }
//    }
//}
