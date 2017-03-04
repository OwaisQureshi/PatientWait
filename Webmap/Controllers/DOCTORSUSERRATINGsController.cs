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
    public class DOCTORSUSERRATINGsController : Controller
    {
        private DBPATIENTNOWAITDataEntities db = new DBPATIENTNOWAITDataEntities();

        // GET: DOCTORSUSERRATINGs
        public ActionResult Index()
        {
            var dOCTORSUSERRATINGS = db.DOCTORSUSERRATINGS.Include(d => d.DOCTOR);
            return View(dOCTORSUSERRATINGS.ToList());
        }

        // GET: DOCTORSUSERRATINGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTORSUSERRATING dOCTORSUSERRATING = db.DOCTORSUSERRATINGS.Find(id);
            if (dOCTORSUSERRATING == null)
            {
                return HttpNotFound();
            }
            return View(dOCTORSUSERRATING);
        }

        // GET: DOCTORSUSERRATINGs/Create
        public ActionResult Create()
        {
            ViewBag.DOCTORID = new SelectList(db.DOCTORS, "DOCTORID", "DOCTORNAME");
            return View();
        }

        // POST: DOCTORSUSERRATINGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USERRATINGID,DOCTORID,DOCTORNAME,ClinicName,USERID,USERNAME,USERCOMMENTS,USERSearchDATETIME")] DOCTORSUSERRATING dOCTORSUSERRATING)
        {
            if (ModelState.IsValid)
            {
                db.DOCTORSUSERRATINGS.Add(dOCTORSUSERRATING);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DOCTORID = new SelectList(db.DOCTORS, "DOCTORID", "DOCTORNAME", dOCTORSUSERRATING.DOCTORID);
            return View(dOCTORSUSERRATING);
        }

        // GET: DOCTORSUSERRATINGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTORSUSERRATING dOCTORSUSERRATING = db.DOCTORSUSERRATINGS.Find(id);
            if (dOCTORSUSERRATING == null)
            {
                return HttpNotFound();
            }
            ViewBag.DOCTORID = new SelectList(db.DOCTORS, "DOCTORID", "DOCTORNAME", dOCTORSUSERRATING.DOCTORID);
            return View(dOCTORSUSERRATING);
        }

        // POST: DOCTORSUSERRATINGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USERRATINGID,DOCTORID,DOCTORNAME,ClinicName,USERID,USERNAME,USERCOMMENTS,USERSearchDATETIME")] DOCTORSUSERRATING dOCTORSUSERRATING)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOCTORSUSERRATING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DOCTORID = new SelectList(db.DOCTORS, "DOCTORID", "DOCTORNAME", dOCTORSUSERRATING.DOCTORID);
            return View(dOCTORSUSERRATING);
        }

        // GET: DOCTORSUSERRATINGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCTORSUSERRATING dOCTORSUSERRATING = db.DOCTORSUSERRATINGS.Find(id);
            if (dOCTORSUSERRATING == null)
            {
                return HttpNotFound();
            }
            return View(dOCTORSUSERRATING);
        }

        // POST: DOCTORSUSERRATINGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DOCTORSUSERRATING dOCTORSUSERRATING = db.DOCTORSUSERRATINGS.Find(id);
            db.DOCTORSUSERRATINGS.Remove(dOCTORSUSERRATING);
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
