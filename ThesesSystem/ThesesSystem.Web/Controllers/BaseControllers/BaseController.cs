namespace ThesesSystem.Web.Controllers.BaseControllers
{
    using System.Web.Mvc;
    using ThesesSystem.Models;
    using ThesesSystem.Web.ViewModels;

    public abstract class BaseController : Controller
    {
        protected User CurrentUser { get; set; }

    }
}