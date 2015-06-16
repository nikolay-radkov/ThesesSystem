namespace ThesesSystem.Web.Controllers
{
    using AutoMapper;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.ViewModels.Faculties;

    public class FacultyController : AuthorizeController
    {
        public FacultyController(IThesesSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult FacultyProfile(int id)
        {
            var faculty = this.Data.Faculties.GetById(id);

            var facultyViewModel = Mapper.Map<FacultyProfileViewModel>(faculty);

            return View(facultyViewModel);
        }
    }
}