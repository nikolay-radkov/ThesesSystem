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
using ThesesSystem.Web.ViewModels.Version;
using ThesesSystem.Web.Infrastructure.StorageFiles;
using System.IO;

namespace ThesesSystem.Web.Controllers
{
    public class ThesisController : AuthorizeController
    {
        private LoggerCreator loggerCreator;
        private IStorage storage;

        public ThesisController(IThesesSystemData data, LoggerCreator loggerCreater, IStorage storage)
            : base(data)
        {
            this.loggerCreator = loggerCreater;
            this.storage = storage;
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
                var thesisViewModel = Mapper.Map<DevThesisTimelineViewModel>(thesis);
                thesisViewModel.ThesisLogs = thesisViewModel.ThesisLogs.OrderByDescending(l => l.CreatedOn).ToList();

                return View(thesisViewModel);
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

        [HttpGet]
        public ActionResult AddVersion(int id)
        {
            var userId = this.User.Identity.GetUserId();

            var thesis = this.Data.Theses.GetById(id);

            if (thesis.SupervisorId == userId || thesis.StudentId == userId)
            {
                var versionViewModel = Mapper.Map<CreateVersionViewModel>(thesis);

                return View(versionViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Storage");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVersion(int id, CreateVersionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: add storage service
                var userId = this.User.Identity.GetUserId();
                var directory = string.Format(@".\{0}\{1}\{2}\", userId, model.Id, DateTime.Now);
                directory = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                directory = directory.Replace(':', '-');
                
                //var path = storage.Save(model.Archive, directory);
                //if (path == null)
                //{
                //    return View(model);
                //}
                string fullpath = null;
                if (model.Archive != null && model.Archive.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(model.Archive.FileName);
                    fullpath = Path.Combine(directory, fileName);

                    model.Archive.SaveAs(Server.MapPath(fullpath));

                }

                var version = new ThesesSystem.Models.Version()
                {
                    ThesisId = model.Id,
                    StoragePath = fullpath
                };

                this.Data.Versions.Add(version);

                this.Data.SaveChanges();

                var logger = this.loggerCreator.Create(this.Data);

                logger.Log(new ThesisLog
                {
                    ThesisId = model.Id,
                    UserId = userId,
                    LogType = LogType.AddedVersion,
                    ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "Version", model.Id)
                });

                return RedirectToAction("Version", "Thesis", new { id = id });
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult DownloadFile(int? id)
        {
            //TODO: Get thesis path
            return File(Server.MapPath("~/Files/") + id, "image/jpeg");
        }
    }
}