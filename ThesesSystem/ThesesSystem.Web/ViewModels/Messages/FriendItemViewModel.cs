namespace ThesesSystem.Web.ViewModels.Messages
{
    using System;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class FriendItemViewModel
    {
        public string FromUserId { get; set; }

        public string FromUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsSeen { get; set; }
    }
}