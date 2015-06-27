namespace ThesesSystem.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Areas.Administration.ViewModels.Theses;
    using ThesesSystem.Web.Areas.Administration.ViewModels.Users;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Storage;

    public class ThesesController : AdministrationController
    {
        private IStorage storage;

        public ThesesController(IThesesSystemData data, IStorage storage)
            : base(data)
        {
            this.storage = storage;
        }

        private void CreateSelectLists()
        {
            var students = this.Data.Users.All()
                            .Where(u => u.Student != null)
                            .Project()
                            .To<UserDropDownItemViewModel>()
                            .ToList();

            var teachers = this.Data.Users.All()
                          .Where(u => u.Teacher != null)
                          .Project()
                          .To<UserDropDownItemViewModel>()
                          .ToList();

            ViewBag.StudentId = new SelectList(students, "Id", "FullName");
            ViewBag.SupervisorId = new SelectList(teachers, "Id", "FullName");
            ViewBag.ReviewerId = new SelectList(teachers, "Id", "FullName");
        }

        public ActionResult Index()
        {
            var theses = this.Data.Theses.All()
                                .Project()
                                .To<ThesisViewModel>()
                                .ToList();

            return View(theses);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thesis = this.Data.Theses.GetById(id);

            if (thesis == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ThesisDetailViewModel>(thesis);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            this.CreateSelectLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThesisCreateViewModel model)
        {
            if (ModelState.IsValid 
                && (model.ThesisArchive != null && model.ThesisArchive.ContentLength > 0)
                && (model.ReviewArchive != null && model.ReviewArchive.ContentLength > 0))
            {
                try
                {
                    byte[] byteArray = null;

                    using (var memory = new MemoryStream())
                    {
                        model.ThesisArchive.InputStream.CopyTo(memory);
                        byteArray = memory.GetBuffer();
                    }

                    var fileName = string.Format(GlobalPatternConstants.VERSION_NAME, DateTime.Now.ToUniversalTime(), model.ThesisArchive.FileName);
                    var fullPath = storage.UploadFile(byteArray, fileName, GlobalConstants.STORAGE_FOLDER);
                    var extensionStartIndex = model.ThesisArchive.FileName.LastIndexOf('.');
                    var fileExtension = model.ThesisArchive.FileName.Substring(extensionStartIndex + 1, model.ThesisArchive.FileName.Length - extensionStartIndex - 1).ToLower();

                    var thesis = Mapper.Map<Thesis>(model);
                    thesis.IsComplete = true;
                    thesis.Versions.Add(new ThesesSystem.Models.Version
                    {        
                        StoragePath = fullPath,
                        FileName = fileName,
                        FileExtension = fileExtension
                    });

                    byteArray = null;

                    using (var memory = new MemoryStream())
                    {
                        model.ReviewArchive.InputStream.CopyTo(memory);
                        byteArray = memory.GetBuffer();
                    }

                    fileName = string.Format(GlobalPatternConstants.VERSION_NAME, DateTime.Now.ToUniversalTime(), model.ReviewArchive.FileName);
                    fullPath = storage.UploadFile(byteArray, fileName, GlobalConstants.STORAGE_FOLDER);
                    extensionStartIndex = model.ReviewArchive.FileName.LastIndexOf('.');
                    fileExtension = model.ReviewArchive.FileName.Substring(extensionStartIndex + 1, model.ReviewArchive.FileName.Length - extensionStartIndex - 1).ToLower();

                    thesis.Evaluation = new Evaluation
                    {
                        FilePath = fullPath,
                        FileName = fileName,
                        FileExtension = fileExtension,
                        ReviewerId = model.ReviewerId,
                        Mark = model.Mark
                    };

                    this.Data.Theses.Add(thesis);
                    this.Data.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    this.CreateSelectLists();
                    return View(model);
                }
            }
        
            this.CreateSelectLists();
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

            this.Data.Theses.Delete(id);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DownloadFile(int id)
        {
            var thesis = this.Data.Theses.GetById(id);

            var version = thesis.Versions.OrderByDescending(v => v.CreatedOn).FirstOrDefault();

            if (this.User.IsInRole(GlobalConstants.TEACHER))
            {
                var mimeType = MimeTypeCreator.GetFileMimeType(version.FileExtension);
                var fileBytes = storage.DownloadFile(version.StoragePath);

                var ms = new MemoryStream(fileBytes);

                return File(fileBytes, mimeType, version.FileName);
            }

            return RedirectToAction("Index", "Storage");
        }
    }
}
