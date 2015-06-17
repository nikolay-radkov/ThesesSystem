namespace ThesesSystem.Web.ViewModels.Notifications
{
    using System.Collections.Generic;
    using System.Linq;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class UserNotificationsViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public int NotSeenNotificationsCount { get; set; }

        public ICollection<NotificationViewModel> Notifications { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<User, UserNotificationsViewModel>()
                .ForMember(u => u.NotSeenNotificationsCount, opt => opt.MapFrom(u => u.Notifications.Where(n => !n.IsSeen).Count()));
        }
    }
}