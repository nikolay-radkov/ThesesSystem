namespace ThesesSystem.Web.Areas.Administration.ViewModels.ThesisTutorials
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisTutorialUpdateViewModel : IMapFrom<ThesisTutorial>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Документ")]
        public HttpPostedFileBase Archive { get; set; }

        [Display(Name = "Преподавател")]
        public string TeacherName { get; set; }

        [Display(Name = "Създадена на")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Преподавател")]
        public string TeacherId { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ThesisTutorial, ThesisTutorialUpdateViewModel>()
               .ForMember(u => u.TeacherName, opt => opt.MapFrom(u => u.Teacher.User.FirstName + " " + u.Teacher.User.LastName));
        }
    }
}