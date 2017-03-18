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
                smtp.Send("patientnowait@gmail.com", "abhijitbavdhankar@gmail.com", "Subject - Test Mail", "Email Body - Test Mail");               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var result = new FilePathResult("~/Views/Home/Hospital.html", "text/html");
            return result;
        }

        //public ActionResult Email(string patientname, string patientemail, string doctorname, string doctoremail, string appdate, string apptime)
        //public ActionResult Emails(string patientemail)
        [HttpPost]
        public ActionResult Emails(FormCollection form)
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("patientnowait", "kangaroo1234");
                smtp.EnableSsl = true;                                
                //smtp.Send("patientnowait@gmail.com", patientemail, "Subject - Test Mail", "Email Body - Test Mail" + patientname);

                string patientname = Convert.ToString(Request["txtPatientName"].ToString());
                string patientemail = Convert.ToString(Request["txtPatientEmailAddress"].ToString());
                String doctorname = Convert.ToString(Request["txtDoctorName"].ToString());
                String appdate = Convert.ToString(Request["txtAppointementDate"].ToString());
                String apptime = Convert.ToString(Request["txtAppointementTime"].ToString());

                smtp.Send("patientnowait@gmail.com", patientemail, "Subject - Test Mail", "Email Body - Test Mail");

                //var data2 = $('#txt2').val();
                //var strConfirmAppointemnt = "Dear {0} Your Appointement with Dr. {1} has been confirmed on date {3} and time {4}.";
                //var strFinalMessage = String.Format(strConfirmAppointemnt, patientname, doctorname, appdate, apptime);
                //smtp.Send("patientnowait@gmail.com", patientemail, "Subject - Appointment Confirmation", strConfirmAppointemnt);
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