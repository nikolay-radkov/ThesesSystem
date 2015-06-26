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
    using ThesesSystem.Web.Areas.Administration.ViewModels.ThesisTutorials;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Storage;
    public class ThesisTutorialsController : AdministrationController
    {
        private IStorage storage;

        public ThesisTutorialsController(IThesesSystemData data, IStorage storage)
            : base(data)
        {
            this.storage = storage;
        }

        public ActionResult Index()
        {
            var thesisTutorials = this.Data.ThesisTutorials.All()
                                 .Project()
                                 .To<ThesisTutorialViewModel>()
                                 .ToList();

            return View(thesisTutorials);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thesisTutorial = this.Data.ThesisTutorials.GetById(id);

            if (thesisTutorial == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ThesisTutorialDetailViewModel>(thesisTutorial);

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thesisTutorial = this.Data.ThesisTutorials.GetById(id);

            if (thesisTutorial == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<ThesisTutorialUpdateViewModel>(thesisTutorial);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ThesisTutorialUpdateViewModel model)
        {
            if (ModelState.IsValid && model.Archive != null && model.Archive.ContentLength > 0)
            {
                try
                {
                    byte[] byteArray = null;

                    using (var memory = new MemoryStream())
                    {
                        model.Archive.InputStream.CopyTo(memory);
                        byteArray = memory.GetBuffer();
                    }

                    var fileName = string.Format(GlobalPatternConstants.VERSION_NAME, DateTime.Now.ToUniversalTime(), model.Archive.FileName);
                    var fullPath = storage.UploadFile(byteArray, fileName, GlobalConstants.STORAGE_FOLDER);

                    var thesisTutorial = this.Data.ThesisTutorials.GetById(model.Id);
                    thesisTutorial.FileName = fileName;
                    thesisTutorial.FilePath = fullPath;

                    this.Data.ThesisTutorials.Update(thesisTutorial);
                    this.Data.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View(model);
                }

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

            this.Data.ThesisTutorials.Delete(id);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DownloadTutorial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tutorial = this.Data.ThesisTutorials.GetById(id);

            if (tutorial == null)
            {
                return HttpNotFound();
            }

            var mimeType = MimeTypeCreator.GetFileMimeType(string.Empty);
            var fileBytes = storage.DownloadFile(tutorial.FilePath);

            var ms = new MemoryStream(fileBytes);

            return File(fileBytes, mimeType, tutorial.FileName);
        }
    }
}
