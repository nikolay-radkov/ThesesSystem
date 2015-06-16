namespace ThesesSystem.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Helper;
    using ThesesSystem.Web.Infrastructure.Storage;
    using ThesesSystem.Web.ViewModels.Theses;

    public class StorageController : AuthorizeController
    {
        private IStorage storage;

        public StorageController(IThesesSystemData data, IStorage storage)
            : base(data)
        {
            this.storage = storage;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            //TODO: implement searching and filtering
           
            int currentPage = page ?? 0;

            var theses = this.Data.Theses
                                    .All()
                                    .Where(u => u.IsComplete)
                                    .OrderBy(t => t.FinishedAt)
                                    .Skip(GlobalConstants.ELEMENTS_PER_PAGE * currentPage)
                                    .Take(GlobalConstants.ELEMENTS_PER_PAGE)
                                    .AsQueryable()
                                    .Project()
                                    .To<ThesisViewModel>()
                                    .ToList();

            ViewData["Pagination"] = PaginationHelper.CreatePagination("Index", "Storage", this.Data, currentPage);

            return View(theses);
        }

        [HttpGet]
        public ActionResult ThesisProfile(int id)
        {
            var thesis = this.Data.Theses.GetById(id);

            var thesisViewModel = Mapper.Map<ThesisProfileViewModel>(thesis);

            return View(thesisViewModel);
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