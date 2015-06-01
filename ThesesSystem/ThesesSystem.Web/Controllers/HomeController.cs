using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Web.ViewModels.Home;

namespace ThesesSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IThesesSystemData data)
            :base(data)
        {

        }

        public ActionResult Index()
        {
            var home = new HomeViewModel();

            return View(home);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
         
            return View();
        }
    }
}