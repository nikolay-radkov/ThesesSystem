using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Models;
using ThesesSystem.Web.Controllers.BaseControllers;

namespace ThesesSystem.Web.Controllers
{
    public class IdeaController : AuthorizeController
    {
        public IdeaController(IThesesSystemData data)
            : base (data)
        {
        }

        // GET: Idea
        [HttpGet]
        public ActionResult Themes()
        {
            //TODO: get supervisors' theme ideas
            return View();
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
            //TODO: implement creating a them only for teachers
            return View();
        }

        [HttpPost]
        public ActionResult Create(ThesisTheme model)
        {
            //TODO: add the nwe theme and return to the themes
            //TODO: Change theme model
            return View();
        }
    }
}