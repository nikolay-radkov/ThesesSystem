namespace ThesesSystem.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.ViewModels.Faculties;
    using ThesesSystem.Web.ViewModels.Partials;
    using ThesesSystem.Web.ViewModels.Specialties;
    using ThesesSystem.Web.ViewModels.Users;

    public class ValidationController : BaseController
    {
        public ValidationController(IThesesSystemData data)
            : base(data)
        {

        }

        [NonAction]
        private void CreateNotification(string id)
        {
            var notification = new Notification
            {
                UserId = id,
                ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_URL, "Storage", "Index"),
                Text = GlobalPatternConstants.NOTIFICATION_USER_COMPLETE
            };

            this.SendNotification(notification);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.NOT_VERIFIED_USER)]
        public async Task<ActionResult> NotVerified()
        {
            var id = this.User.Identity.GetUserId();
            var user = this.Data.Users.GetById(id);

            if (user == null)
            {
                return new HttpStatusCodeResult(400, "Bad Request");
            }

            await this.SignInUser(user);

            return View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.NOT_COMPLETE_USER)]
        public ActionResult CompleteStudentRegistration(string id)
        {
            var user = this.Data.Users.GetById(id);

            if (user == null)
            {
                return new HttpStatusCodeResult(400, "Bad Request");
            }

            var userViewModel = Mapper.Map<UserProfileViewModel>(user);
            var faculties = this.Data.Faculties.All()
                                    .AsQueryable()
                                    .Project()
                                    .To<FacultyDropDownListItemViewModel>()
                                    .ToList();


            ViewBag.FacultyId = new SelectList(faculties, "Id", "Title");

            return View(userViewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.NOT_COMPLETE_USER)]
        public ActionResult CompleteTeacherRegistration(string id)
        {
            var user = this.Data.Users.GetById(id);

            if (user == null)
            {
                return new HttpStatusCodeResult(400, "Bad Request");
            }

            var userViewModel = Mapper.Map<UserProfileViewModel>(user);

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.NOT_COMPLETE_USER)]
        public async Task<ActionResult> CompleteStudentRegistration(string id, StudentRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.Data.Users.GetById(id);

                if (user == null)
                {
                    return new HttpStatusCodeResult(400, "Bad Request");
                }

                var student = Mapper.Map<Student>(model);
                student.Id = id;

                var userStore = new UserStore<User>(this.Data.Context.DbContext);
                var userManager = new UserManager<User>(userStore);

                this.Data.Students.Add(student);
                this.Data.SaveChanges();

                userManager.AddToRole(user.Id, GlobalConstants.COMPLETE_USER);
                userManager.RemoveFromRole(user.Id, GlobalConstants.NOT_COMPLETE_USER);

                this.Data.SaveChanges();

                await this.SignInUser(user);

                CreateNotification(id);

                return RedirectToAction("Index", "Storage");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.NOT_COMPLETE_USER)]
        public async Task<ActionResult> CompleteTeacherRegistration(string id, TeacherInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.Data.Users.GetById(id);

                if (user == null)
                {
                    return new HttpStatusCodeResult(400, "Bad Request");
                }

                var teacher = Mapper.Map<Teacher>(model);
                teacher.Id = id;

                var userStore = new UserStore<User>(this.Data.Context.DbContext);
                var userManager = new UserManager<User>(userStore);

                this.Data.Teachers.Add(teacher);
                this.Data.SaveChanges();

                userManager.AddToRole(user.Id, GlobalConstants.COMPLETE_USER);
                userManager.RemoveFromRole(user.Id, GlobalConstants.NOT_COMPLETE_USER);

                this.Data.SaveChanges();

                await this.SignInUser(user);

                CreateNotification(id);

                return RedirectToAction("Index", "Storage");
            }

            return View();
        }


        [HttpGet]
        public JsonResult GetSpecialties(int id)
        {
            var specialties = this.Data.Specialties.All()
                                    .Where(s => s.FacultyId == id)
                                    .AsQueryable()
                                    .Project()
                                    .To<SpecialtyDropDownListItemViewModel>()
                                    .ToList();

            return Json(specialties, JsonRequestBehavior.AllowGet);
        }


        [NonAction]
        private async Task SignInUser(User user)
        {
            var signInManager = Request.GetOwinContext()
                .Get<ApplicationSignInManager>();
            await signInManager.SignInAsync(user, false, false);
        }
    }
}