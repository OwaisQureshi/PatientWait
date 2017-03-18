using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webmap.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Render HTML files
        public ActionResult Hospital()
        {
            var result = new FilePathResult("~/Views/Home/Hospital.html", "text/html");
            return result;
        }

        public ActionResult MyHtml(string htmlPageName)
        {
            var result = new FilePathResult($"~/Views/{htmlPageName}.html", "text/html");
            return result;
        }
    }
}