namespace ThesesSystem.Web.Areas.Administration.ViewModels.ThesisThemes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisThemeViewModel : IMapFrom<ThesisTheme>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Преподавател")]
        public string TeacherName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ThesisTheme, ThesisThemeViewModel>()
               .ForMember(u => u.TeacherName, opt => opt.MapFrom(u => u.Teacher.User.FirstName + " " + u.Teacher.User.LastName));
        }
    }
}