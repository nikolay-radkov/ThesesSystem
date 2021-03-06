﻿using System.Web.Mvc;
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

        [OutputCache(Duration = 2 * 60 * 60)]
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

        [HttpGet]
        public ActionResult Error()
        {
            // <customErrors mode="On" defaultRedirect="/Home/Error"/></customErrors>
            return View();
        }
    }
}