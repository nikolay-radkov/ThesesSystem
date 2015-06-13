namespace ThesesSystem.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Helper;
    using ThesesSystem.Web.ViewModels.ThesisTheme;
    using Microsoft.AspNet.Identity;
    using ThesesSystem.Web.ViewModels.Theses;
    using AutoMapper;

    public class IdeaController : AuthorizeController
    {
        public IdeaController(IThesesSystemData data)
            : base(data)
        {
        }

        // GET: Idea
        [HttpGet]
        public ActionResult Themes(int? page)
        {
            int currentPage = page ?? 0;

            var themes = this.Data.ThesisThemes.All()
                            .OrderBy(t => t.CreatedOn)
                            .Skip(GlobalConstants.ELEMENTS_PER_PAGE * currentPage)
                            .Take(GlobalConstants.ELEMENTS_PER_PAGE)
                          .Project()
                          .To<ThesisThemeViewModel>()
                          .ToList();

            ViewData["Pagination"] = PaginationHelper.CreatePagination("Themes", "Idea", this.Data, currentPage);

            return View(themes);
        }

        [HttpGet]
        public ActionResult Generator()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetWord()
        {
            if (this.Request.IsAjaxRequest())
            {
                var title = this.Data.ThesisThemes.All()
                                .OrderBy(t => Guid.NewGuid())
                                .Select(t => t.Title)
                                .FirstOrDefault();

                if (title == null)
                {
                    title = this.Data.Theses.All()
                                .OrderBy(t => Guid.NewGuid())
                                .Select(t => t.Title)
                                .FirstOrDefault();

                    //TODO: if title == null get word from faculty
                }

                var pattern = @"\W+";

                var word = Regex.Split(title, pattern)
                                .OrderBy(x => Guid.NewGuid())
                                .Select(x => x)
                                .FirstOrDefault();

                return Json(new { Word = word }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Generator");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (this.User.IsInRole(GlobalConstants.TEACHER))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Themes");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CreateThesisThemeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.User.IsInRole(GlobalConstants.TEACHER))
                {
                    var theme = Mapper.Map<ThesisTheme>(model);
                    theme.TeacherId = this.User.Identity.GetUserId();

                    this.Data.ThesisThemes.Add(theme);

                    this.Data.SaveChanges();
                }

                return RedirectToAction("Themes");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Take(int id)
        {
            var theme = this.Data.ThesisThemes.All()
                     .FirstOrDefault(t => t.Id == id);

            if (theme == null || theme.IsTaken)
            {
                return new HttpStatusCodeResult(400, "Bad Request");
            }

            ViewData["SupervisorName"] = string.Format("{0} {1}", theme.Teacher.User.FirstName, theme.Teacher.User.LastName);

            var thesis = new CreateThesisViewModel
            {
                SupervisorId = theme.TeacherId,
                Title = theme.Title
            };

            return View(thesis);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Take(int id, CreateThesisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var thesis = Mapper.Map<Thesis>(model);
                thesis.StudentId = userId;

                this.Data.Theses.Add(thesis);

                var theme = this.Data.ThesisThemes.All()
                      .FirstOrDefault(t => t.Id == id);
                theme.IsTaken = true;

                this.Data.SaveChanges();

                return RedirectToAction("ThesisProfile", "Thesis", new { id = thesis.Id });
            }

            return View(model);
        }
    }
}