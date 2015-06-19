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
    public class ThesisTutorialsController : Controller
    {
        private ThesesSystemDbContext db = new ThesesSystemDbContext();

        // GET: Administration/ThesisTutorials
        public ActionResult Index()
        {
            var thesisTutorials = db.ThesisTutorials.Include(t => t.Teacher);
            return View(thesisTutorials.ToList());
        }

        // GET: Administration/ThesisTutorials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisTutorial thesisTutorial = db.ThesisTutorials.Find(id);
            if (thesisTutorial == null)
            {
                return HttpNotFound();
            }
            return View(thesisTutorial);
        }

        // GET: Administration/ThesisTutorials/Create
        public ActionResult Create()
        {
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber");
            return View();
        }

        // POST: Administration/ThesisTutorials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FilePath,FileName,TeacherId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] ThesisTutorial thesisTutorial)
        {
            if (ModelState.IsValid)
            {
                db.ThesisTutorials.Add(thesisTutorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesisTutorial.TeacherId);
            return View(thesisTutorial);
        }

        // GET: Administration/ThesisTutorials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisTutorial thesisTutorial = db.ThesisTutorials.Find(id);
            if (thesisTutorial == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesisTutorial.TeacherId);
            return View(thesisTutorial);
        }

        // POST: Administration/ThesisTutorials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FilePath,FileName,TeacherId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] ThesisTutorial thesisTutorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thesisTutorial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "OfficePhoneNumber", thesisTutorial.TeacherId);
            return View(thesisTutorial);
        }

        // GET: Administration/ThesisTutorials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisTutorial thesisTutorial = db.ThesisTutorials.Find(id);
            if (thesisTutorial == null)
            {
                return HttpNotFound();
            }
            return View(thesisTutorial);
        }

        // POST: Administration/ThesisTutorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThesisTutorial thesisTutorial = db.ThesisTutorials.Find(id);
            db.ThesisTutorials.Remove(thesisTutorial);
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
