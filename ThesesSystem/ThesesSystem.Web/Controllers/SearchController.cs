using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesesSystem.Data;
using ThesesSystem.Web.Controllers.BaseControllers;
using ThesesSystem.Web.ViewModels.Search;

namespace ThesesSystem.Web.Controllers
{
    public class SearchController : AuthorizeController
    {
        public SearchController(IThesesSystemData data)
            : base(data)
        {

        }

        // GET: Search
        public ActionResult Index(SearchViewModel model)
        {
            // TODO: Implement search page with found students, supervisors, theses and so on.

            return View();
        }
    }
}