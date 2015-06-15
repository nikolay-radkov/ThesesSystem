namespace ThesesSystem.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Helper;
    using ThesesSystem.Web.ViewModels.Theses;

    public class StorageController : AuthorizeController
    {
        public StorageController(IThesesSystemData data)
            : base(data)
        {
        }

        // GET: Storage
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
            // TODO: Implement downloading

            var thesis = this.Data.Theses.GetById(id);

            var thesisViewModel = Mapper.Map<ThesisProfileViewModel>(thesis);

            return View(thesisViewModel);
        }
    }
}