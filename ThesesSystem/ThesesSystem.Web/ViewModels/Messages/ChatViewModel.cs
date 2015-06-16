namespace ThesesSystem.Web.ViewModels.Messages
{
    using System.Collections.Generic;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ChatViewModel
    {
        public ICollection<FriendItemViewModel> FriendsList { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }
    }
}