using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThesesSystem.Web.Infrastructure.Mapping;

namespace ThesesSystem.Web.ViewModels.Message
{
    public class ChatViewModel
    {
        public ICollection<FriendItemViewModel> FriendsList { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }
    }
}