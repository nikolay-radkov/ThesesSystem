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
    using ThesesSystem.Web.ViewModels.Specialties;
    using ThesesSystem.Web.ViewModels.Students;

    public class SpecialtyController : AuthorizeController
    {
        public SpecialtyController(IThesesSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult SpecialtyProfile(int id, int? page)
        {
            var specialty = this.Data.Specialties.GetById(id);
            int currentPage = page ?? 0;

            var students = this.Data.Students.All()
                               .Where(s => s.SpecialtyId == id)
                             .OrderBy(s => s.User.FirstName)
                             .ThenBy(s => s.User.MiddleName)
                             .ThenBy(s=> s.User.LastName)
                            .Skip(GlobalConstants.ELEMENTS_PER_PAGE * currentPage)
                            .Take(GlobalConstants.ELEMENTS_PER_PAGE)
                            .AsQueryable()
                            .Project()
                            .To<SpecialtyStudentViewModel>()
                            .ToList();

            ViewData["Pagination"] = PaginationHelper.CreatePagination("SpecialtyProfile", "Specialty", this.Data, currentPage, id);

            var specialtyViewModel = Mapper.Map<SpecialtyProfileViewModel>(specialty);
            specialtyViewModel.StudentsList = students;

            return View(specialtyViewModel);
        }
    }
}