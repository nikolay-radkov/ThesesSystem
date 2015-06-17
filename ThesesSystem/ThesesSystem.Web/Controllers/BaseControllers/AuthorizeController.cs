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
        protected void SendNotification(Notification notification)
        {
            this.Data.Notifications.Add(notification);
            this.Data.SaveChanges();

            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            var userId = NotificationHub.GetConnectionUserId(notification.UserId);

            var notificationViewModel = Mapper.Map<NotificationViewModel>(notification);
            notificationViewModel.UserId = userId;

            if (userId != null)
            {
                context.Clients.Client(userId).addNotification(notificationViewModel);
            }
        }

        [NonAction]
        protected string GetUserFullName(string userId)
        {
            var user = this.Data.Users.GetById(userId);
            var userFullName = string.Format(GlobalPatternConstants.USER_FULL_NAME, user.FirstName, user.LastName);

            return userFullName;
        }
    }
}