using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhino.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tags()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult UploadContent()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewContent()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}