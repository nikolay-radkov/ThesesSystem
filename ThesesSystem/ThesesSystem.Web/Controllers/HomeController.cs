using System.Web.Mvc;
using System.Linq;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Web.ViewModels.Home;

namespace ThesesSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IThesesSystemData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Storage");
            }
            else
            {
                var home = new HomeViewModel();

                home.ThesesNumber = this.Data.Theses.All().Count();
                home.ThesisThemesNumber = this.Data.ThesisThemes.All().Count();
                home.UsersNumber = this.Data.Users.All().Count();

                return View(home);
            }
        }

        public ActionResult Feedback()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Error()
        {
            // TODO: add custom errors
            // <customErrors mode="On" defaultRedirect="/Home/Error"/></customErrors>
            return View();
        }
    }
}