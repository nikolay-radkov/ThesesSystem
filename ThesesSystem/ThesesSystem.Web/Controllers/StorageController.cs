namespace ThesesSystem.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using ThesesSystem.Data;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.ViewModels.Theses;

    [Authorize]
    public class StorageController : BaseController
    {
        public StorageController(IThesesSystemData data)
            : base(data)
        {

        }

        // GET: Storage
        [HttpGet]
        public ActionResult Index(int? page)
        {
            int currentPage = page ?? 0;

            var theses = this.Data.Theses
                                    .All()
                                    .OrderBy(t => t.FinishedAt)
                                    .Skip(GlobalConstants.ELEMENTS_PER_PAGE * currentPage)
                                    .Take(GlobalConstants.ELEMENTS_PER_PAGE)
                                    .AsQueryable()
                                    .Project()
                                    .To<ThesisViewModel>()
                                    .ToList();

            return View(theses);
        }
    }
}