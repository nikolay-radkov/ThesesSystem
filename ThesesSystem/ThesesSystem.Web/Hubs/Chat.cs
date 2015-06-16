using AutoMapper;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThesesSystem.Data;
using ThesesSystem.Models;
using ThesesSystem.Web.ViewModels.Messages;

namespace ThesesSystem.Web.Hubs
{
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
            message.CreatedOn = DateTime.Now;

            var messageToSave = Mapper.Map<Message>(message);

            if (ConnectedUsers.ContainsKey(message.ToUserId))
            {
                messageToSave.IsSeen = true;

                var toUserId = ConnectedUsers[message.ToUserId];
                Clients.Client(toUserId).AddMessage(message);
                Clients.Client(Context.ConnectionId).ShowMessage(message);
            }

            data.Messages.Add(messageToSave);
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