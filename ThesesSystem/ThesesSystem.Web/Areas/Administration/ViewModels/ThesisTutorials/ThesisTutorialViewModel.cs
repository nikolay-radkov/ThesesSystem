namespace ThesesSystem.Web.Areas.Administration.ViewModels.ThesisTutorials
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisTutorialViewModel : IMapFrom<ThesisTutorial>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Наръчник")]
        public string FileName { get; set; }


        [Display(Name = "Преподавател")]
        public string TeacherName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ThesisTutorial, ThesisTutorialViewModel>()
               .ForMember(u => u.TeacherName, opt => opt.MapFrom(u => u.Teacher.User.FirstName + " " + u.Teacher.User.LastName));
        }
    }
}