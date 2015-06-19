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
    using ThesesSystem.Web.ViewModels.Filters;
    using ThesesSystem.Web.ViewModels.Theses;

    public class StorageController : AuthorizeController
    {
        private IStorage storage;
        private bool isKeyWordCorrect = true;

        public StorageController(IThesesSystemData data, IStorage storage)
            : base(data)
        {
            this.storage = storage;
        }

        [NonAction]
        private IQueryable<Thesis> SetFilterClause(IQueryable<Thesis> theses, FilterBy filterBy, string keyWord)
        {
            switch (filterBy)
            {
                case FilterBy.FacultyNumber:
                    long number = 0;
                    if (long.TryParse(keyWord, out number))
                    {
                        theses = theses.Where(t => t.Student.FacultyNumber == number);
                    }
                    else
                    {
                        isKeyWordCorrect = false;
                    }
                    break;
                case FilterBy.EGN:
                    long egn = 0;
                    if (long.TryParse(keyWord, out egn))
                    {
                        theses = theses.Where(t => t.Student.User.EGN == egn);
                    }
                    else
                    {
                        isKeyWordCorrect = false;
                    }
                    break;
                case FilterBy.StudentName:
                    theses = theses.Where(t => t.Student.User.FirstName.Contains(keyWord)
                                                        || t.Student.User.MiddleName.Contains(keyWord)
                                                        || t.Student.User.LastName.Contains(keyWord));
                    break;
                case FilterBy.TeacherName:
                    theses = theses.Where(t => t.Supervisor.User.FirstName.Contains(keyWord)
                                                        || t.Supervisor.User.MiddleName.Contains(keyWord)
                                                        || t.Supervisor.User.LastName.Contains(keyWord));
                    break;
                case FilterBy.Specialty:
                    theses = theses.Where(t => t.Student.Specialty.Title.Contains(keyWord));
                    break;
                case FilterBy.Faculty:
                    theses = theses.Where(t => t.Student.Specialty.Faculty.Title.Contains(keyWord));
                    break;
                case FilterBy.Thesis:
                    theses = theses.Where(t => t.Title.Contains(keyWord));
                    break;
                case FilterBy.Description:
                    theses = theses.Where(t => t.Description.Contains(keyWord));
                    break;
            }

            return theses;
        }

        [NonAction]
        private IQueryable<Thesis> SetSortAscendingClause(IQueryable<Thesis> theses, SortBy sortBy)
        {
            switch (sortBy)
            {
                case SortBy.All:
                    theses = theses.OrderBy(t => t.Title);
                    break;
                case SortBy.FacultyNumber:
                    theses = theses.OrderBy(t => t.Student.FacultyNumber);
                    break;
                case SortBy.EGN:
                    theses = theses.OrderBy(t => t.Student.User.EGN);
                    break;
                case SortBy.StudentName:
                    theses = theses.OrderBy(t => t.Student.User.FirstName)
                                .ThenBy(t => t.Student.User.MiddleName)
                                .ThenBy(t => t.Student.User.LastName);
                    break;
                case SortBy.TeacherName:
                    theses = theses.OrderBy(t => t.Supervisor.User.FirstName)
                              .ThenBy(t => t.Supervisor.User.MiddleName)
                              .ThenBy(t => t.Supervisor.User.LastName);
                    break;
                case SortBy.Specialty:
                    theses = theses.OrderBy(t => t.Student.Specialty.Title);
                    break;
                case SortBy.Faculty:
                    theses = theses.OrderBy(t => t.Student.Specialty.Faculty.Title);
                    break;
                case SortBy.FinishedDate:
                    theses = theses.OrderBy(t => t.FinishedAt);
                    break;
                case SortBy.FinalEvaluation:
                    theses = theses.OrderBy(t => t.FinalEvaluation);
                    break;
                case SortBy.Thesis:
                    theses = theses.OrderBy(t => t.Title);
                    break;
            }

            return theses;
        }

        [NonAction]
        private IQueryable<Thesis> SetSortDescendingClause(IQueryable<Thesis> theses, SortBy sortBy)
        {
            switch (sortBy)
            {
                case SortBy.All:
                    theses = theses.OrderByDescending(t => t.Title);
                    break;
                case SortBy.FacultyNumber:
                    theses = theses.OrderByDescending(t => t.Student.FacultyNumber);
                    break;
                case SortBy.EGN:
                    theses = theses.OrderByDescending(t => t.Student.User.EGN);
                    break;
                case SortBy.StudentName:
                    theses = theses.OrderByDescending(t => t.Student.User.FirstName)
                                .ThenBy(t => t.Student.User.MiddleName)
                                .ThenBy(t => t.Student.User.LastName);
                    break;
                case SortBy.TeacherName:
                    theses = theses.OrderByDescending(t => t.Supervisor.User.FirstName)
                              .ThenBy(t => t.Supervisor.User.MiddleName)
                              .ThenBy(t => t.Supervisor.User.LastName);
                    break;
                case SortBy.Specialty:
                    theses = theses.OrderByDescending(t => t.Student.Specialty.Title);
                    break;
                case SortBy.Faculty:
                    theses = theses.OrderByDescending(t => t.Student.Specialty.Faculty.Title);
                    break;
                case SortBy.FinishedDate:
                    theses = theses.OrderByDescending(t => t.FinishedAt);
                    break;
                case SortBy.FinalEvaluation:
                    theses = theses.OrderByDescending(t => t.FinalEvaluation);
                    break;
                case SortBy.Thesis:
                    theses = theses.OrderByDescending(t => t.Title);
                    break;
            }

            return theses;
        }

        [HttpGet]
        public ActionResult Index(int? page, FilterOptions options)
        {
            int currentPage = page ?? 0;
            var theses = this.Data.Theses
                                    .All()
                                    .Where(u => u.IsComplete);
            if (options != null)
            {
                if (options.KeyWord != null)
                {
                    theses = this.SetFilterClause(theses, options.FilterBy, options.KeyWord);

                }

                if (options.SortType == SortType.Ascending)
                {
                    theses = this.SetSortAscendingClause(theses, options.SortBy);
                }
                else
                {
                    theses = this.SetSortDescendingClause(theses, options.SortBy);
                }
            }
            else
            {
                theses = theses.OrderBy(t => t.FinishedAt);
            }

            var thesesViewModel = theses
                .Skip(GlobalConstants.ELEMENTS_PER_PAGE * currentPage)
                .Take(GlobalConstants.ELEMENTS_PER_PAGE)
                .Project()
                .To<ThesisViewModel>()
                .ToList();

            ViewData["FilterOptions"] = options;
            ViewData["Pagination"] = PaginationHelper.CreatePagination("Index", "Storage", this.Data, currentPage, options: options);
            ViewData["IsKeyWordCorrect"] = this.isKeyWordCorrect;

            return View(thesesViewModel);
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