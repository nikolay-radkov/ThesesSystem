﻿namespace ThesesSystem.Web.Areas.Administration.ViewModels.Users
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class UserDropDownItemViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<User, UserDropDownItemViewModel>()
                .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.FirstName + " " + u.MiddleName + " " + u.LastName));
        }
    }
}