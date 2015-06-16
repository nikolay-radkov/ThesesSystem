namespace ThesesSystem.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Helper;
    using ThesesSystem.Web.ViewModels.Partials;
    using ThesesSystem.Web.ViewModels.Users;

    public class UserController : AuthorizeController
    {
        public UserController(IThesesSystemData data)
            : base(data)
        {
        }

        // GET: User/Account
        [HttpGet]
        public ActionResult Account(string id)
        {
            var user = this.Data.Users.GetById(id);
            var userViewModel = Mapper.Map<UserProfileViewModel>(user);
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.Id == currentUserId);

            userViewModel.IsFriend = currentUser.Friends.Where(u => u.Id == id).Count() != 0;

            return View(userViewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.ADMIN)]
        public ActionResult Verification(int? page)
        {
            var currentPage = page ?? 0;


            var usersToVerify = this.Data.Users.All()
                                        .Where(u => !u.IsVerified)
                                        .OrderBy(t => t.FirstName)
                                        .Skip(GlobalConstants.ELEMENTS_PER_PAGE * currentPage)
                                        .Take(GlobalConstants.ELEMENTS_PER_PAGE)
                                        .Project()
                                        .To<VerificationViewModel>()
                                        .ToList();

            ViewData["Pagination"] = PaginationHelper.CreatePagination("Verification", "User", this.Data, currentPage);

            return View(usersToVerify);
        }

        [HttpGet]
        public ActionResult StudentInfo(string id)
        {
            var student = this.Data.Students.GetById(id);
            var studentViewModel = Mapper.Map<StudentInfoViewModel>(student);

            return PartialView("_StudentInfoPartial", studentViewModel);
        }

        [HttpGet]
        public ActionResult TeacherInfo(string id)
        {
            var teacher = this.Data.Teachers.GetById(id);
            var teacherViewModel = Mapper.Map<TeacherInfoViewModel>(teacher);

            return PartialView("_TeacherInfoPartial", teacherViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles=GlobalConstants.ADMIN)]
        public ActionResult Verify(string id, VerificationType verificationType)
        {
            var user = this.Data.Users.GetById(id);

            var userStore = new UserStore<User>(this.Data.Context.DbContext);
            var userManager = new UserManager<User>(userStore);
        

            // TODO: Implement logic for studen and teacher validation
            switch (verificationType)
            {
                case VerificationType.Student:
                    user.IsVerified = true;
                    userManager.AddToRole(user.Id, GlobalConstants.NOT_COMPLETE_USER);
                    userManager.AddToRole(user.Id, GlobalConstants.VERIFIED_USER);
                    userManager.AddToRole(user.Id, GlobalConstants.STUDENT);
                    userManager.RemoveFromRole(user.Id, GlobalConstants.NOT_VERIFIED_USER);
                    break;
                case VerificationType.Teacher:
                    user.IsVerified = true;
                    userManager.AddToRole(user.Id, GlobalConstants.NOT_COMPLETE_USER);
                    userManager.AddToRole(user.Id, GlobalConstants.VERIFIED_USER);
                    userManager.AddToRole(user.Id, GlobalConstants.TEACHER);
                    userManager.RemoveFromRole(user.Id, GlobalConstants.NOT_VERIFIED_USER);
                    break;
                case VerificationType.Bot:
                    this.Data.Users.Delete(user);
                    this.Data.SaveChanges();
                    return RedirectToAction("Verification", "User", new { id = id });
                default:
                    break;
            }

            this.Data.SaveChanges();

            return RedirectToAction("Account", "User", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewFriend(string id)
        {
            var user = this.Data.Users.GetById(id);

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.Id == currentUserId);

            currentUser.Friends.Add(user);

            this.Data.SaveChanges();

            return RedirectToAction("Account", "User", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFriend(string id)
        {
            var user = this.Data.Users.GetById(id);

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(u => u.Id == currentUserId);

            currentUser.Friends.Remove(user);

            this.Data.SaveChanges();

            return RedirectToAction("Account", "User", new { id = id });
        }
    }
}