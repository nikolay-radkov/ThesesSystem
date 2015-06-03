namespace ThesesSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Web.Controllers.BaseControllers;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Helper;
    using ThesesSystem.Web.ViewModels.Users;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    [Authorize]
    public class UserController : BaseController
    {
        public UserController(IThesesSystemData data)
            :base (data)
        {

        }

        // GET: User/Account
        [HttpGet]
        public ActionResult Account(string id)
        {
            // adim -> verify user
            // loking teacher profile
            // loking user profile
           
            // Add/Remove friend
            // Send message

            return View();
        }

        [HttpGet]
        [Authorize(Roles=GlobalConstants.ADMIN)]
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
    }
}