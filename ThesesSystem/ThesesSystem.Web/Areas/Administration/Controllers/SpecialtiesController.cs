namespace ThesesSystem.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Areas.Administration.ViewModels.Specialties;
    using ThesesSystem.Web.Controllers.BaseControllers;

    public class SpecialtiesController : AdministrationController
    {
        public SpecialtiesController(IThesesSystemData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var specialties = this.Data.Specialties.All()
                                .Project()
                                .To<SpecialtyViewModel>()
                                .ToList();

            return View(specialties);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var specialty = this.Data.Specialties.GetById(id);

            if (specialty == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<SpecialtyUpdateViewModel>(specialty);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(this.Data.Faculties.All(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpecialtyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var specialty = Mapper.Map<Specialty>(model);
                this.Data.Specialties.Add(specialty);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var specialty = this.Data.Specialties.GetById(id);
      
            if (specialty == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<SpecialtyUpdateViewModel>(specialty);

            ViewBag.FacultyId = new SelectList(this.Data.Faculties.All(), "Id", "Title", specialty.FacultyId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpecialtyUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var specialty = Mapper.Map<Specialty>(model);

                this.Data.Specialties.Update(specialty);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // POST: Administration/Specialties/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.Data.Specialties.Delete(id);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
