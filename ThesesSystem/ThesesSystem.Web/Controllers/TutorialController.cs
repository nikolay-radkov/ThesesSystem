using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Web.Infrastructure.Constants;
using ThesesSystem.Web.Infrastructure.Storage;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ThesesSystem.Web.ViewModels.ThesisTutorial;
using ThesesSystem.Web.Infrastructure.Helper;
using Microsoft.AspNet.Identity;
using ThesesSystem.Models;

namespace ThesesSystem.Web.Controllers
{
    public class TutorialController : AuthorizeController
    {
        private IStorage storage;
        public TutorialController(IThesesSystemData data, IStorage storage)
            : base(data)
        {
            this.storage = storage;
        }

        // GET: Tutorial
        public ActionResult Index(int? page)
        {
            int currentPage = page ?? 0;

            var tutorials = this.Data.ThesisTutorials
                                    .All()
                                    .OrderBy(t => t.CreatedOn)
                                    .Skip(GlobalConstants.ELEMENTS_PER_PAGE * currentPage)
                                    .Take(GlobalConstants.ELEMENTS_PER_PAGE)
                                    .AsQueryable()
                                    .Project()
                                    .To<TutorialViewModel>()
                                    .ToList();

            ViewData["Pagination"] = PaginationHelper.CreatePagination("Index", "Tutorial", this.Data, currentPage);

            return View(tutorials);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (User.IsInRole(GlobalConstants.TEACHER))
            {
                var model = new CreateTutorialViewModel();

                return View(model);
            }

            return RedirectToAction("Index", "Tutorial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTutorialViewModel model)
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
                    var userId = this.User.Identity.GetUserId();

                    var tutorial = new ThesisTutorial()
                    {
                        FilePath = fullPath,
                        TeacherId = userId,
                        FileName = fileName
                    };

                    this.Data.ThesisTutorials.Add(tutorial);
                    this.Data.SaveChanges();
                }
                catch (Exception)
                {
                    return View(model);
                }

                return RedirectToAction("Index", "Tutorial");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult DownloadTutorial(int id)
        {
            var tutorial = this.Data.ThesisTutorials.GetById(id);

            var mimeType = MimeTypeCreator.GetFileMimeType(string.Empty);
            var fileBytes = storage.DownloadFile(tutorial.FilePath);

            var ms = new MemoryStream(fileBytes);

            return File(fileBytes, mimeType, tutorial.FileName);
        }
    }
}