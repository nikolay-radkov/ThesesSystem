using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Data;
using ThesesSystem.Web.ViewModels.Theses;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ThesesSystem.Web.Controllers
{
    public class ThesisController : AuthorizeController
    {
        public ThesisController(IThesesSystemData data)
            : base(data)
        {
        }

        // GET: Thesis
        [HttpGet]
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();

            var thesesViewModel = this.Data.Theses.All()
                                    .Where(t => t.StudentId == userId || t.SupervisorId == userId)
                                    .Project()
                                    .To<DevThesisIndexViewModel>()
                                    .ToList();

            return View(thesesViewModel);
        }
    }
}