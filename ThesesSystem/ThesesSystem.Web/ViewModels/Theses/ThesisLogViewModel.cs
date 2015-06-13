namespace ThesesSystem.Web.ViewModels.Theses
{
    using System;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisLogViewModel : IMapFrom<ThesisLog>, IHaveCustomMappings
    {
        public string ForwardUrl { get; set; }

        public LogType LogType { get; set; }

        public string UserFullName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ThesisLog, ThesisLogViewModel>()
              .ForMember(u => u.UserFullName, opt => opt.MapFrom(u => u.User.FirstName + " " + u.User.LastName));
        }
    }
}