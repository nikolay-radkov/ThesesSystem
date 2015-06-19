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
    public class ThesisThemesController : Controller
    {
        private ThesesSystemDbContext db = new ThesesSystemDbContext();

        // GET: Administration/ThesisThemes
        public ActionResult Index()
        {
            var thesisThemes = db.ThesisThemes.Include(t => t.Teacher);
            return View(thesisThemes.ToList());
        }

        // GET: Administration/ThesisThemes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisTheme thesisTheme = db.ThesisThemes.Find(id);
            if (thesisTheme == null)
            {
                return HttpNotFound();
            }
            return View(thesisTheme);
        }

        // GET: Administration/ThesisThemes/Create
        public ActionResult Create()
        {
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber");
            return View();
        }

        // POST: Administration/ThesisThemes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,IsTaken,TeacherId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] ThesisTheme thesisTheme)
        {
            if (ModelState.IsValid)
            {
                db.ThesisThemes.Add(thesisTheme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesisTheme.TeacherId);
            return View(thesisTheme);
        }

        // GET: Administration/ThesisThemes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisTheme thesisTheme = db.ThesisThemes.Find(id);
            if (thesisTheme == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesisTheme.TeacherId);
            return View(thesisTheme);
        }

        // POST: Administration/ThesisThemes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IsTaken,TeacherId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] ThesisTheme thesisTheme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thesisTheme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesisTheme.TeacherId);
            return View(thesisTheme);
        }

        // GET: Administration/ThesisThemes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisTheme thesisTheme = db.ThesisThemes.Find(id);
            if (thesisTheme == null)
            {
                return HttpNotFound();
            }
            return View(thesisTheme);
        }

        // POST: Administration/ThesisThemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThesisTheme thesisTheme = db.ThesisThemes.Find(id);
            db.ThesisThemes.Remove(thesisTheme);
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
