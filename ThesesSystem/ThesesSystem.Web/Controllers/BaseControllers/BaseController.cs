namespace ThesesSystem.Web.Controllers.BaseControllers
{
    using System.Web.Mvc;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.ViewModels.Partials;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using ThesesSystem.Web.Hubs;
    using AutoMapper;
    using ThesesSystem.Web.ViewModels.Notifications;

    public abstract class BaseController : Controller
    {
        public BaseController(IThesesSystemData data)
        {
            this.Data = data;
        }

        protected IThesesSystemData Data { get; set; }

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
    }
}