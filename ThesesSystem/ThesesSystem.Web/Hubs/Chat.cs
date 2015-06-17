namespace ThesesSystem.Web.Hubs
{
    using AutoMapper;
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Collections.Generic;
    using ThesesSystem.Data;
    using ThesesSystem.Models;
    using ThesesSystem.Web.ViewModels.Messages;

    public class Chat : Hub
    {
        private static IDictionary<string, string> ConnectedUsers = new Dictionary<string, string>();
        private static IThesesSystemData data = new ThesesSystemData(new ThesesSystemDbContext());

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

                if (ConnectedUsers.ContainsKey(message.ToUserId))
                {
                    messageToSave.IsSeen = true;
                    message.IsSeen = true;

                    var toUserId = ConnectedUsers[message.ToUserId];
                    Clients.Client(toUserId).AddMessage(message);
                }

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