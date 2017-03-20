using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Webmap.Models;

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

        public ActionResult Email()
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("patientnowait", "kangaroo1234");
                smtp.EnableSsl = true;
                smtp.Send("patientnowait@gmail.com", "abhijitbavdhankar@gmail.com", "Subject - Pateint No Wait Mail", "Email Body - Pateint No WaitTest Mail");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var result = new FilePathResult("~/Views/Home/Hospital.html", "text/html");
            return result;
        }
    }
}