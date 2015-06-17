namespace ThesesSystem.Web.ViewModels.Notifications
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string ForwardUrl { get; set; }

        public bool IsSeen { get; set; }

        public string UserId { get; set; }
    }
}