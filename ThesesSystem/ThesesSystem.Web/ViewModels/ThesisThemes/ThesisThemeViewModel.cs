namespace ThesesSystem.Web.ViewModels.ThesisThemes
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisThemeViewModel : IMapFrom<ThesisTheme>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsTaken { get; set; }

        public string TeacherName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ThesisTheme, ThesisThemeViewModel>()
               .ForMember(u => u.TeacherName, opt => opt.MapFrom(u => u.Teacher.User.FirstName + " " + u.Teacher.User.LastName));
        }
    }
}