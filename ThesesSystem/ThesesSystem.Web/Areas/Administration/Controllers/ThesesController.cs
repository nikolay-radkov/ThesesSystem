using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Models;

namespace ThesesSystem.Web.Areas.Administration.Controllers
{
    public class ThesesController : Controller
    {
        private ThesesSystemDbContext db = new ThesesSystemDbContext();

        // GET: Administration/Theses
        public ActionResult Index()
        {
            var theses = db.Theses.Include(t => t.Evaluation).Include(t => t.Student).Include(t => t.Supervisor);
            return View(theses.ToList());
        }

        // GET: Administration/Theses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.Theses.Find(id);
            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        // GET: Administration/Theses/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Evaluations, "Id", "FilePath");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Id");
            ViewBag.SupervisorId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber");
            return View();
        }

        // POST: Administration/Theses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,FinishedAt,Pages,FinalEvaluation,IsComplete,SupervisorId,StudentId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Thesis thesis)
        {
            if (ModelState.IsValid)
            {
                db.Theses.Add(thesis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Evaluations, "Id", "FilePath", thesis.Id);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Id", thesis.StudentId);
            ViewBag.SupervisorId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesis.SupervisorId);
            return View(thesis);
        }

        // GET: Administration/Theses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.Theses.Find(id);
            if (thesis == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Evaluations, "Id", "FilePath", thesis.Id);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Id", thesis.StudentId);
            ViewBag.SupervisorId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesis.SupervisorId);
            return View(thesis);
        }

        // POST: Administration/Theses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,FinishedAt,Pages,FinalEvaluation,IsComplete,SupervisorId,StudentId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Thesis thesis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thesis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Evaluations, "Id", "FilePath", thesis.Id);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Id", thesis.StudentId);
            ViewBag.SupervisorId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesis.SupervisorId);
            return View(thesis);
        }

        // GET: Administration/Theses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thesis thesis = db.Theses.Find(id);
            if (thesis == null)
            {
                return HttpNotFound();
            }
            return View(thesis);
        }

        // POST: Administration/Theses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thesis thesis = db.Theses.Find(id);
            db.Theses.Remove(thesis);
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
