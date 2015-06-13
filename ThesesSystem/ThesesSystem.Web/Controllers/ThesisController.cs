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
using ThesesSystem.Web.ViewModels.Teacher;
using ThesesSystem.Models;
using ThesesSystem.Web.Infrastructure.Factories.Logger;
using ThesesSystem.Web.Infrastructure.Constants;

namespace ThesesSystem.Web.Controllers
{
    public class ThesisController : AuthorizeController
    {
        private LoggerCreator loggerCreator;

        public ThesisController(IThesesSystemData data, LoggerCreator loggerCreater)
            : base(data)
        {
            this.loggerCreator = loggerCreater;
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

        [HttpGet]
        public ActionResult ThesisProfile(int id)
        {   
            // TODO: Add more new parts
            // TODO: delete the thesis
            // TODO: Add reviewer and admin
           
            var userId = this.User.Identity.GetUserId();

            var thesis = this.Data.Theses.GetById(id);

            if (thesis.SupervisorId == userId || thesis.StudentId == userId)
            {
             // TODO: implement logic

                var logs = this.Data.ThesisLogs.All()
                                .Where(t => t.ThesisId == id)
                                .Project()
                                .To<ThesisLogViewModel>()
                                .ToList();

                return View(logs);
            }
            else
            {
                return RedirectToAction("Index", "Storage");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var newThesis = new CreateThesisViewModel();
            var superviosors = this.Data.Teachers.All()
                                  .AsQueryable()
                                  .Project()
                                  .To<SupervisorDropDownListITemViewModel>()
                                  .ToList();

            ViewBag.SupervisorId = new SelectList(superviosors, "Id", "FullName");

            return View(newThesis);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateThesisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                var thesis = Mapper.Map<Thesis>(model);
                thesis.StudentId = userId;

                this.Data.Theses.Add(thesis);

                this.Data.SaveChanges();

                var logger = this.loggerCreator.Create(this.Data);
               
                logger.Log(new ThesisLog
                {
                    ThesisId = thesis.Id,
                    UserId = userId,
                    LogType = LogType.CreatedThesis,
                    ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "ThesisProfile", thesis.Id)
                });

                return RedirectToAction("ThesisProfile", "Thesis", new { id = thesis.Id });
            }

            var superviosors = this.Data.Teachers.All()
                               .AsQueryable()
                               .Project()
                               .To<SupervisorDropDownListITemViewModel>()
                               .ToList();


            ViewBag.SupervisorId = new SelectList(superviosors, "Id", "FullName");

            return View(model);
        }

    }
}