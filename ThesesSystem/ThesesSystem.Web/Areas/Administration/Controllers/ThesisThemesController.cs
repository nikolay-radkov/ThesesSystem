namespace ThesesSystem.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Areas.Administration.ViewModels.ThesisThemes;
    using ThesesSystem.Web.Controllers.BaseControllers;

    public class ThesisThemesController : AdministrationController
    {

        public ThesisThemesController(IThesesSystemData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var thesisThemes = this.Data.ThesisThemes.All()
                                .Project()
                                .To<ThesisThemeViewModel>()
                                .ToList();

            return View(thesisThemes);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thesisTheme = this.Data.ThesisThemes.GetById(id);

            if (thesisTheme == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ThesisThemeDetailViewModel>(thesisTheme);

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thesisTheme = this.Data.ThesisThemes.GetById(id);

            if (thesisTheme == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ThesisThemeDetailViewModel>(thesisTheme);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ThesisThemeDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var thesisTheme = Mapper.Map<ThesisTheme>(model);

                this.Data.ThesisThemes.Update(thesisTheme);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            this.Data.ThesisThemes.Delete(id);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
