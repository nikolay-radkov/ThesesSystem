namespace ThesesSystem.Web.Hubs
{
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using ThesesSystem.Data;
    using ThesesSystem.Web.ViewModels.Notifications;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using AutoMapper;

    public class NotificationHub : Hub
    {
        private static IDictionary<string, string> ConnectedUsers = new Dictionary<string, string>();
        private static IThesesSystemData data = new ThesesSystemData(new ThesesSystemDbContext());

        public static string GetConnectionUserId(string modelId)
        {
            if (ConnectedUsers.ContainsKey(modelId))
            {
                return ConnectedUsers[modelId];
            }

            return null;
        }

        public void Connect(string userId)
        {
            var id = Context.ConnectionId;

            if (!ConnectedUsers.ContainsKey(id) && !ConnectedUsers.ContainsKey(userId))
            {
                ConnectedUsers.Add(id, userId);
                ConnectedUsers.Add(userId, id);
            }
        }

        public void GetNotificationHistory(string userId)
        {
            var user = data.Users.GetById(userId);
            var userNotifications = Mapper.Map <UserNotificationsViewModel>(user);
            userNotifications.Notifications = userNotifications.Notifications.OrderByDescending(n => n.CreatedOn).ToList();
            Clients.Client(Context.ConnectionId).showNotificationHistory(userNotifications);
        }

        public void MarkAsSeenNotification (int notificationId)
        {
            var notification = data.Notifications.GetById(notificationId);
            notification.IsSeen = true;

            data.SaveChanges();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;

            if (ConnectedUsers.ContainsKey(id))
            {
                var userId = ConnectedUsers[id];
                ConnectedUsers.Remove(id);

                if (ConnectedUsers.ContainsKey(userId))
                {

                    ConnectedUsers.Remove(userId);
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}