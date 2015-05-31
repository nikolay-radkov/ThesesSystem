using ThesesSystem.Data;
namespace ThesesSystem.Web.Controllers.BaseControllers
{
    //[Authorize(Roles="Admin")]
    public abstract class AdmininistrationController : BaseController
    {
        public AdmininistrationController(IThesesSystemData data) 
            : base (data)
        { 
        
        }
    }
}