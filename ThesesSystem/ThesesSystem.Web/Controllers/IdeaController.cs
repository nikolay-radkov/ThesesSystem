namespace ThesesSystem.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Factories.Logger;
    using ThesesSystem.Web.Infrastructure.Helper;
    using ThesesSystem.Web.ViewModels.Theses;
    using ThesesSystem.Web.ViewModels.ThesisThemes;

    public class IdeaController : AuthorizeController
    {
        private LoggerCreator loggerCreator;

        public IdeaController(IThesesSystemData data, LoggerCreator loggerCreator)
            : base(data)
        {
            this.loggerCreator = loggerCreator;
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
            if (this.User.IsInRole(GlobalConstants.STUDENT))
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

            return RedirectToAction("Themes", "Idea");
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

                var logger = this.loggerCreator.Create(this.Data);

                logger.Log(new ThesisLog
                {
                    ThesisId = thesis.Id,
                    UserId = userId,
                    LogType = LogType.CreatedThesis,
                    ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "ThesisProfile", thesis.Id)
                });

                this.Data.SaveChanges();

                return RedirectToAction("ThesisProfile", "Thesis", new { id = thesis.Id });
            }

            return View(model);
        }
    }
}