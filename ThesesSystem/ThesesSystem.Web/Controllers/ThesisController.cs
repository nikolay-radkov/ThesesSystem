namespace ThesesSystem.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Factories.Logger;
    using ThesesSystem.Web.Infrastructure.Storage;
    using ThesesSystem.Web.ViewModels.Comments;
    using ThesesSystem.Web.ViewModels.Teacher;
    using ThesesSystem.Web.ViewModels.Theses;
    using ThesesSystem.Web.ViewModels.ThesisPart;
    using ThesesSystem.Web.ViewModels.Version;

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

            // TODO: Add this check for every action !!!
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
            if (ModelState.IsValid && model.Archive != null && model.Archive.ContentLength > 0)
            {
                var userId = this.User.Identity.GetUserId();
                var versionId = 0;

                try
                {
                    versionId = SaveNewVersion(model);
                }
                catch (Exception)
                {
                    return View(model);
                }

                UpdateParts(model.Id, model.ThesisParts);

                var logger = this.loggerCreator.Create(this.Data);

                logger.Log(new ThesisLog
                {
                    ThesisId = model.Id,
                    UserId = userId,
                    LogType = LogType.AddedVersion,
                    ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "Version", versionId)
                });

                return RedirectToAction("Version", "Thesis", new { id = versionId });
            }

            return View(model);
        }

        [NonAction]
        private void UpdateParts(int id, IList<CreateOrUpdateThesisPartViewModel> thesisParts)
        {
            var parts = this.Data.ThesisParts.All().Where(p => p.ThesisId == id).ToList();

            int index = 0;
            for (index = 0; index < parts.Count; index++)
            {
                parts[index].Flag = thesisParts[index].Flag;
            }

            for (int i = index; i < thesisParts.Count; i++)
            {
                var part = Mapper.Map<ThesisPart>(thesisParts[index]);
                part.ThesisId = id;

                this.Data.ThesisParts.Add(part);
            }

            this.Data.SaveChanges();
        }

        [NonAction]
        private int SaveNewVersion(CreateVersionViewModel model)
        {
            byte[] byteArray = null;

            using (var memory = new MemoryStream())
            {
                model.Archive.InputStream.CopyTo(memory);
                byteArray = memory.GetBuffer();
            }

            var fileName = string.Format(GlobalPatternConstants.VERSION_NAME, DateTime.Now.ToUniversalTime(), model.Archive.FileName);
            var fullPath = storage.UploadFile(byteArray, fileName, GlobalConstants.STORAGE_FOLDER);
            var extensionStartIndex = model.Archive.FileName.LastIndexOf('.');
            var fileExtension = model.Archive.FileName.Substring(extensionStartIndex + 1, model.Archive.FileName.Length - extensionStartIndex - 1).ToLower();

            var version = new ThesesSystem.Models.Version()
            {
                ThesisId = model.Id,
                StoragePath = fullPath,
                FileName = model.Archive.FileName,
                FileExtension = fileExtension
            };

            this.Data.Versions.Add(version);
            this.Data.SaveChanges();

            return version.Id;
        }

        [NonAction]
        private string GetFileMimeType(string fileExtension)
        {
            switch (fileExtension)
            {
                case "rar":
                    return GlobalConstants.RAR_MIME_TYPE;
                case "7z":
                    return GlobalConstants.SEVENZ_MIME_TYPE;
                case "zip":
                    return GlobalConstants.ZIP_MIME_TYPE;
                case "bz":
                    return GlobalConstants.BZIP_MIME_TYPE;
                case "bz2":
                    return GlobalConstants.BZIP2_MIME_TYPE;
                case "tar":
                    return GlobalConstants.TAR_MIME_TYPE;
                default:
                    return System.Net.Mime.MediaTypeNames.Application.Octet;
            }
        }


        [HttpGet]
        public ActionResult Version(int id)
        {
            var version = this.Data.Versions.GetById(id);
            var versionViewModel = Mapper.Map<VersionProfileViewModel>(version);
            versionViewModel.Comments = versionViewModel.Comments.OrderByDescending(c => c.CreatedOn).ToList();

            return View(versionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Version(int id, CreateCommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();

                var newComment = Mapper.Map<Comment>(comment);
                newComment.UserId = userId;
                newComment.VersionId = id;

                this.Data.Comments.Add(newComment);
                this.Data.SaveChanges();

                var logger = this.loggerCreator.Create(this.Data);

                var version = this.Data.Versions.GetById(id);

                logger.Log(new ThesisLog
                {
                    ThesisId = version.ThesisId,
                    UserId = userId,
                    LogType = LogType.AddedComment,
                    ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "Version", id)
                });

                return RedirectToAction("Version", "Thesis", new { id = id });
            }

            return View(comment);
        }


        [HttpGet]
        public ActionResult DownloadFile(int id)
        {
            var version = this.Data.Versions.GetById(id);
            var mimeType = GetFileMimeType(version.FileExtension);
            var fileBytes = storage.DownloadFile(version.StoragePath);

            var ms = new MemoryStream(fileBytes);

            return File(fileBytes, mimeType, version.FileName);
        }

        [HttpGet]
        public ActionResult AddPart(int id)
        {
            var partViewModel = this.Data.ThesisParts.All()
                                .Where(p => p.ThesisId == id)
                                .Project()
                                .To<CreateOrUpdateThesisPartViewModel>()
                                .ToList();

            return View(partViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPart(int id, IList<CreateOrUpdateThesisPartViewModel> parts)
        {
            if (ModelState.IsValid)
            {
                UpdateParts(id, parts);
                var userId = this.User.Identity.GetUserId();
                var logger = this.loggerCreator.Create(this.Data);

                logger.Log(new ThesisLog
                {
                    ThesisId = id,
                    UserId = userId,
                    LogType = LogType.AddedPart,
                    ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "ThesisProfile", id)
                });

                return RedirectToAction("ThesisProfile", "Thesis", new { id = id });
            }

            return View(parts);
        }
    }
}