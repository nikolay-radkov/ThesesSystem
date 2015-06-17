namespace ThesesSystem.Web.Controllers.BaseControllers
{
    using AutoMapper;
    using Microsoft.AspNet.SignalR;
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Hubs;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.Infrastructure.Filters;
    using ThesesSystem.Web.ViewModels.Notifications;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    [CompleteUserAttribute]
    public abstract class AuthorizeController : BaseController
    {
        public AuthorizeController(IThesesSystemData data)
            : base(data)
        {
        }

        [NonAction]
        protected string GetUserFirstName(string userId)
        {
            var user = this.Data.Users.GetById(userId);
            var firstName = user.FirstName;

            return firstName;
        }
    }
}