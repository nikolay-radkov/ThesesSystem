namespace ThesesSystem.Web.ViewModels.ThesisTutorial
{
    using System;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class TutorialViewModel : IMapFrom<ThesisTutorial>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string TeacherId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FileName { get; set; }

        public string TeacherName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ThesisTutorial, TutorialViewModel>()
               .ForMember(u => u.TeacherName, opt => opt.MapFrom(u => u.Teacher.User.FirstName + " " + u.Teacher.User.LastName));
        }
    }
}