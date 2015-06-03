using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;

namespace ThesesSystem.Web.Controllers
{
    [Authorize]
    public class SearchController : BaseController
    {
        public SearchController(IThesesSystemData data)
            : base(data)
        {

        }

        // GET: Search
        public ActionResult Index()
        {
            // TODO: Implement search page with found students, supervisors, theses and so on.

            return View();
        }
    }
}