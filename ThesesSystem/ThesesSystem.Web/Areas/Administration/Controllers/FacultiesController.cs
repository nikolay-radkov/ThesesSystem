namespace ThesesSystem.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Areas.Administration.ViewModels.Faculties;
    using ThesesSystem.Web.Controllers.BaseControllers;

    public class FacultiesController : AdministrationController
    {
        public FacultiesController(IThesesSystemData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var faculties = this.Data.Faculties.All()
                               .Project()
                               .To<FacultyViewModel>()
                               .ToList();

            return View(faculties);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var faculty = this.Data.Faculties.GetById(id);

            if (faculty == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<FacultyDetailViewModel>(faculty);

            return View(viewModel);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacultyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var faculty = Mapper.Map<Faculty>(model);
                this.Data.Faculties.Add(faculty);
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

            var faculty = this.Data.Faculties.GetById(id);

            if (faculty == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<FacultyDetailViewModel>(faculty);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FacultyDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var faculty = Mapper.Map<Faculty>(model);

                this.Data.Faculties.Update(faculty);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.Data.Faculties.Delete(id);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
