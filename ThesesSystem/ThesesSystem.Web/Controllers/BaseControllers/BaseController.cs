namespace ThesesSystem.Web.Controllers.BaseControllers
{
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.ViewModels.Partial;

    public abstract class BaseController : Controller
    {
        public BaseController(IThesesSystemData data)
        {
            this.Data = data;
        }

        protected IThesesSystemData Data { get; set; }

        protected User CurrentUser { get; set; }
    }
}