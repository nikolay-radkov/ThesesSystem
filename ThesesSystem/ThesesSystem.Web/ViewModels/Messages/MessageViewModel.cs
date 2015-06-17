namespace ThesesSystem.Web.ViewModels.Messages
{
    using System;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class MessageViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FromUserId { get; set; }

        public string ToUserId { get; set; }
        
        public string ToUserName { get; set; }

        public string FromUserName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ThesesSystem.Models.Message, MessageViewModel>()
             .ForMember(u => u.FromUserName, opt => opt.MapFrom(u => u.FromUser.FirstName + " " + u.FromUser.LastName))
             .ForMember(u => u.ToUserName, opt => opt.MapFrom(u => u.ToUser.FirstName + " " + u.ToUser.LastName));
        }
    }
}