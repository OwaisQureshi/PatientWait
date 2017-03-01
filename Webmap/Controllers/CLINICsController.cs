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
    public class CLINICsController : Controller
    {
        private DBPATIENTNOWAITDataEntities db = new DBPATIENTNOWAITDataEntities();

        // GET: CLINICs
        public ActionResult Index()
        {
            return View(db.CLINICS.ToList());
        }

        // GET: CLINICs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLINIC cLINIC = db.CLINICS.Find(id);
            if (cLINIC == null)
            {
                return HttpNotFound();
            }
            return View(cLINIC);
        }

        // GET: CLINICs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CLINICs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClinicID,CLINICGoogleUID,ClinicName,CLINICADDRESS,ClinicEmail,ClinicTelePhone,CLINICWorkingHours,ClinicWebsite,USERID,USERNAME,USERSearchDATETIME")] CLINIC cLINIC)
        {
            if (ModelState.IsValid)
            {
                db.CLINICS.Add(cLINIC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cLINIC);
        }

        // GET: CLINICs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLINIC cLINIC = db.CLINICS.Find(id);
            if (cLINIC == null)
            {
                return HttpNotFound();
            }
            return View(cLINIC);
        }

        // POST: CLINICs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClinicID,CLINICGoogleUID,ClinicName,CLINICADDRESS,ClinicEmail,ClinicTelePhone,CLINICWorkingHours,ClinicWebsite,USERID,USERNAME,USERSearchDATETIME")] CLINIC cLINIC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLINIC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cLINIC);
        }

        // GET: CLINICs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLINIC cLINIC = db.CLINICS.Find(id);
            if (cLINIC == null)
            {
                return HttpNotFound();
            }
            return View(cLINIC);
        }

        // POST: CLINICs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLINIC cLINIC = db.CLINICS.Find(id);
            db.CLINICS.Remove(cLINIC);
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
