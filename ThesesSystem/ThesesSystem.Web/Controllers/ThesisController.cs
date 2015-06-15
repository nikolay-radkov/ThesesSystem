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
    using ThesesSystem.Web.ViewModels.Evaluation;
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

        [NonAction]
        private bool IsThesisStudentOrTeacher(int thesisId)
        {
            var userId = this.User.Identity.GetUserId();
            var thesis = this.Data.Theses.GetById(thesisId);

            var result = this.IsThesisTeacher(userId, thesis) || this.IsThesisStudent(userId, thesis);

            return result;
        }

        [NonAction]
        private bool IsThesisStudent(string userId, Thesis thesis)
        {
            var result = thesis.StudentId == userId;

            return result;
        }

        [NonAction]
        private bool IsThesisTeacher(string userId, Thesis thesis)
        {
            var result = thesis.SupervisorId == userId || this.User.IsInRole(GlobalConstants.ADMIN);

            return result;
        }

        [NonAction]
        private void UpdatePages(int id, int? pages)
        {
            var thesis = this.Data.Theses.GetById(id);

            thesis.Pages = pages;
            this.Data.SaveChanges();
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

        private int SaveNewReview(CreateReviewViewModel model)
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

            var review = new Evaluation()
            {
                Id = model.Id,
                FilePath = fullPath,
                FileName = model.Archive.FileName,
                FileExtension = fileExtension,
                ReviewerId = model.ReviewerId,
                Mark = model.Mark
            };

            this.Data.Evaluations.Add(review);
            this.Data.SaveChanges();

            return review.Id;
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
            // TODO: delete the thesis
            // TODO: Add reviewer and admin            
            // TODO: Add this check for every action !!!
            if (IsThesisStudentOrTeacher(id))
            {
                var thesis = this.Data.Theses.GetById(id);
                var thesisViewModel = Mapper.Map<DevThesisTimelineViewModel>(thesis);

                thesisViewModel.ThesisLogs = thesisViewModel.ThesisLogs.OrderByDescending(l => l.CreatedOn).ToList();

                return View(thesisViewModel);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (this.User.IsInRole(GlobalConstants.STUDENT))
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

            return RedirectToAction("Index", "Storage");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateThesisViewModel model)
        {
            if (this.User.IsInRole(GlobalConstants.STUDENT))
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

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult AddVersion(int id)
        {
            if (this.IsThesisStudentOrTeacher(id))
            {
                var thesis = this.Data.Theses.GetById(id);
                var versionViewModel = Mapper.Map<CreateVersionViewModel>(thesis);

                return View(versionViewModel);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVersion(int id, CreateVersionViewModel model)
        {
            if (this.IsThesisStudentOrTeacher(id))
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
                    UpdatePages(model.Id, model.Pages);

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

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult Version(int id)
        {
            var version = this.Data.Versions.GetById(id);

            if (this.IsThesisStudentOrTeacher(version.ThesisId))
            {
                var versionViewModel = Mapper.Map<VersionProfileViewModel>(version);
                versionViewModel.Comments = versionViewModel.Comments.OrderByDescending(c => c.CreatedOn).ToList();

                return View(versionViewModel);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Version(int id, CreateCommentViewModel comment)
        {
            var version = this.Data.Versions.GetById(id);
            if (this.IsThesisStudentOrTeacher(version.ThesisId))
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

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult DownloadFile(int id)
        {
            var version = this.Data.Versions.GetById(id);

            if (this.IsThesisStudentOrTeacher(version.ThesisId))
            {
                var mimeType = MimeTypeCreator.GetFileMimeType(version.FileExtension);
                var fileBytes = storage.DownloadFile(version.StoragePath);

                var ms = new MemoryStream(fileBytes);

                return File(fileBytes, mimeType, version.FileName);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult DownloadReviewFile(int id)
        {
            var review = this.Data.Evaluations.GetById(id);

            if (this.IsThesisStudentOrTeacher(id))
            {
                var mimeType = MimeTypeCreator.GetFileMimeType(review.FileExtension);
                var fileBytes = storage.DownloadFile(review.FilePath);

                var ms = new MemoryStream(fileBytes);

                return File(fileBytes, mimeType, review.FileName);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult AddPart(int id)
        {
            if (this.IsThesisStudentOrTeacher(id))
            {
                var partViewModel = this.Data.ThesisParts.All()
                                    .Where(p => p.ThesisId == id)
                                    .Project()
                                    .To<CreateOrUpdateThesisPartViewModel>()
                                    .ToList();

                return View(partViewModel);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPart(int id, IList<CreateOrUpdateThesisPartViewModel> parts)
        {
            if (this.IsThesisStudentOrTeacher(id))
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

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult AddReview(int id)
        {
            var thesis = this.Data.Theses.GetById(id);
            var userId = this.User.Identity.GetUserId();

            if (this.IsThesisTeacher(userId, thesis) && thesis.Evaluation == null)
            {
                var newReview = new CreateReviewViewModel { Id = id };
                var reviewers = this.Data.Teachers.All()
                                  .OrderBy(t => t.User.FirstName)
                                  .ThenBy(t => t.User.LastName)
                                  .AsQueryable()
                                  .Project()
                                  .To<SupervisorDropDownListITemViewModel>()
                                  .ToList();

                ViewBag.ReviewerId = new SelectList(reviewers, "Id", "FullName");

                return View(newReview);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(int id, CreateReviewViewModel model)
        {
            var thesis = this.Data.Theses.GetById(id);
            var userId = this.User.Identity.GetUserId();

            if (this.IsThesisTeacher(userId, thesis) && thesis.Evaluation == null)
            {
                if (ModelState.IsValid)
                {
                    var reviewId = 0;

                    try
                    {
                        reviewId = SaveNewReview(model);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("ThesisProfile", "Thesis", new { id = id });
                    }

                    var logger = this.loggerCreator.Create(this.Data);

                    logger.Log(new ThesisLog
                    {
                        ThesisId = id,
                        UserId = userId,
                        LogType = LogType.AddedReview,
                        ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "ViewReview", reviewId)
                    });

                    return RedirectToAction("ThesisProfile", "Thesis", new { id = id });
                }
                var reviewers = this.Data.Teachers.All()
                                 .OrderBy(t => t.User.FirstName)
                                 .ThenBy(t => t.User.LastName)
                                 .AsQueryable()
                                 .Project()
                                 .To<SupervisorDropDownListITemViewModel>()
                                 .ToList();

                ViewBag.ReviewerId = new SelectList(reviewers, "Id", "FullName");

                return View(model);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult ViewReview(int id)
        {
            if (this.IsThesisStudentOrTeacher(id) && Request.IsAjaxRequest())
            {
                var review = this.Data.Evaluations.GetById(id);
                var reviewViewModel = Mapper.Map<ReviewViewModel>(review);

                return PartialView("_ViewReviewPartial", reviewViewModel);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAsComplete(int id)
        {
            if (this.IsThesisStudentOrTeacher(id))
            {
                var thesis = this.Data.Theses.GetById(id);

                thesis.FinishedAt = DateTime.Now;
                thesis.IsComplete = true;
                this.Data.SaveChanges();

                var logger = this.loggerCreator.Create(this.Data);
                var userId = this.User.Identity.GetUserId();

                logger.Log(new ThesisLog
                {
                    ThesisId = id,
                    UserId = userId,
                    LogType = LogType.CompletedThesis,
                    ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "ThesisProfile", id)
                });

                return RedirectToAction("ThesisProfile", "Thesis", new { id = id });
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult AddFinalEvaluation(int id)
        {
            var thesis = this.Data.Theses.GetById(id);
            var userId = this.User.Identity.GetUserId();

            if (this.IsThesisTeacher(userId, thesis) && thesis.FinalEvaluation == null)
            {
                var finalEvaluation = new CreateFinalEvaluationViewModel { Id = id };
                return View(finalEvaluation);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFinalEvaluation(int id, CreateFinalEvaluationViewModel model)
        {
            var thesis = this.Data.Theses.GetById(id);
            var userId = this.User.Identity.GetUserId();

            if (this.IsThesisTeacher(userId, thesis) && thesis.FinalEvaluation == null)
            {
                if (ModelState.IsValid)
                {
                    thesis.FinishedAt = DateTime.Now;
                    thesis.FinalEvaluation = model.FinalEvaluation;
                    this.Data.SaveChanges();

                    var logger = this.loggerCreator.Create(this.Data);

                    logger.Log(new ThesisLog
                    {
                        ThesisId = id,
                        UserId = userId,
                        LogType = LogType.AddedFinalEvaluation,
                        ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL_WITH_ID, "Thesis", "ViewFinalEvaluation", id)
                    });

                    return RedirectToAction("ThesisProfile", "Thesis", new { id = id });
                }

                return View(model);
            }

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult ViewFinalEvaluation(int id)
        {
            if (this.IsThesisStudentOrTeacher(id) && Request.IsAjaxRequest())
            {
                var thesis = this.Data.Theses.GetById(id);
                var thesisViewModel = Mapper.Map<FinalEvaluationViewModel>(thesis);

                return PartialView("_ViewFinalEvaluationPartial", thesisViewModel);
            }

            return RedirectToAction("Index", "Storage");
        }
    }
}