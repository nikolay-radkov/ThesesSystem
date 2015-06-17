namespace ThesesSystem.Web.Hubs
{
    using AutoMapper;
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Constants;
    using ThesesSystem.Web.ViewModels.Messages;
    using ThesesSystem.Web.ViewModels.Notifications;
    using Common.Extensions;

    public class Chat : Hub
    {
        private static IDictionary<string, string> ConnectedUsers = new Dictionary<string, string>();
        private static IThesesSystemData data = new ThesesSystemData(new ThesesSystemDbContext());

        private void SendNotification(Notification notification)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            var userId = NotificationHub.GetConnectionUserId(notification.UserId);

            var notificationViewModel = Mapper.Map<NotificationViewModel>(notification);
            notificationViewModel.UserId = userId;

            if (userId != null)
            {
                context.Clients.Client(userId).addNotification(notificationViewModel);
            }
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

        public void SendMessage(MessageViewModel message)
        {
            var user = data.Users.GetById(message.ToUserId);
            if (message.ToUserId != null && user != null)
            {
                message.CreatedOn = DateTime.Now;

                var messageToSave = Mapper.Map<Message>(message);
                var notification = new Notification
                {
                    UserId = message.ToUserId,
                    ForwardUrl = string.Format(GlobalPatternConstants.FORWARD_MESSAGE_URL, message.FromUserId),
                    Text = string.Format(GlobalPatternConstants.NOTIFICATION_NEW_MESSAGE, 
                        message.FromUserName.TruncateLongString(GlobalConstants.TRUNCATE_SIZE))
                };
                var isSeen = false;
        

                if (ConnectedUsers.ContainsKey(message.ToUserId))
                {
                    messageToSave.IsSeen = true;
                    message.IsSeen = true;
                    notification.IsSeen = true;
                    isSeen = true;
                    var toUserId = ConnectedUsers[message.ToUserId];
                    Clients.Client(toUserId).AddMessage(message);
                }

                data.Notifications.Add(notification);
                data.Messages.Add(messageToSave);
                data.SaveChanges();

                var historyMessage = new MessageViewModel
                {
                    FromUserId = message.ToUserId,
                    FromUserName = message.ToUserName,
                    Text = message.Text,
                    IsSeen = messageToSave.IsSeen,
                    ToUserId = message.FromUserId,
                    ToUserName = message.FromUserName,
                    CreatedOn = message.CreatedOn
                };

                Clients.Client(Context.ConnectionId).AddToHistory(historyMessage);
                Clients.Client(Context.ConnectionId).ShowMessage(message);

                if(!isSeen)
                {
                     this.SendNotification(notification);
                }
            }
            else
            {
                Clients.Client(Context.ConnectionId).ShowError();
            }
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