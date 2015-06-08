using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThesesSystem.Web.Infrastructure.Mapping;

namespace ThesesSystem.Web.ViewModels.Message
{
    public class MessageViewModel : IMapFrom<ThesesSystem.Models.Message>, IHaveCustomMappings
    {
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FromUserId { get; set; }

        public string ToUserId { get; set; }

        public string FromUserName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ThesesSystem.Models.Message, MessageViewModel>()
             .ForMember(u => u.FromUserName, opt => opt.MapFrom(u => u.FromUser.FirstName + " " + u.FromUser.LastName));
        }
    }
}