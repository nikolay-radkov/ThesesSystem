namespace ThesesSystem.Web.Controllers.BaseControllers
{
    using System.Web.Mvc;
    using ThesesSystem.Data;

    [Authorize(Roles="Admin")]
    public abstract class AdministrationController : AuthorizeController
    {
        public AdministrationController(IThesesSystemData data) 
            : base (data)
        { 
        }
    }
}