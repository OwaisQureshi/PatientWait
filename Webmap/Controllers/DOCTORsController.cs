using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webmap.Models;

namespace Webmap.Controllers
{
    public class DOCTORsController : Controller
    {
        private DBPATIENTNOWAITDataEntities db = new DBPATIENTNOWAITDataEntities();

        // GET: DOCTORs
        public ActionResult Index()
        {
            var dOCTORS = db.DOCTORS.Include(d => d.CLINIC);
            return View(dOCTORS.ToList());
        }

        // GET: DOCTORs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTOR dOCTOR = db.DOCTORS.Find(id);
            if (dOCTOR == null)
            {
                return HttpNotFound();
            }
            return View(dOCTOR);
        }

        // GET: DOCTORs/Create
        public ActionResult Create()
        {
            ViewBag.ClinicID = new SelectList(db.CLINICS, "ClinicID", "CLINICGoogleUID");
            return View();
        }

        // POST: DOCTORs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DOCTORID,DOCTORNAME,DOCTORADDRESS,DOCTOREmail,DOCTORTelePhone,DOCTORWorkingHours,ClinicID,CLINICGoogleUID,ClinicName,USERID,USERNAME,USERSearchDATETIME")] DOCTOR dOCTOR)
        {
            if (ModelState.IsValid)
            {
                db.DOCTORS.Add(dOCTOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClinicID = new SelectList(db.CLINICS, "ClinicID", "CLINICGoogleUID", dOCTOR.ClinicID);
            return View(dOCTOR);
        }

        // GET: DOCTORs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTOR dOCTOR = db.DOCTORS.Find(id);
            if (dOCTOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClinicID = new SelectList(db.CLINICS, "ClinicID", "CLINICGoogleUID", dOCTOR.ClinicID);
            return View(dOCTOR);
        }

        // POST: DOCTORs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DOCTORID,DOCTORNAME,DOCTORADDRESS,DOCTOREmail,DOCTORTelePhone,DOCTORWorkingHours,ClinicID,CLINICGoogleUID,ClinicName,USERID,USERNAME,USERSearchDATETIME")] DOCTOR dOCTOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOCTOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClinicID = new SelectList(db.CLINICS, "ClinicID", "CLINICGoogleUID", dOCTOR.ClinicID);
            return View(dOCTOR);
        }

        // GET: DOCTORs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTOR dOCTOR = db.DOCTORS.Find(id);
            if (dOCTOR == null)
            {
                return HttpNotFound();
            }
            return View(dOCTOR);
        }

        // POST: DOCTORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DOCTOR dOCTOR = db.DOCTORS.Find(id);
            db.DOCTORS.Remove(dOCTOR);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
