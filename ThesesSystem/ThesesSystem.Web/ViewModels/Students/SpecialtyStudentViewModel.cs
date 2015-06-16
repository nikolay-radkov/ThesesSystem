namespace ThesesSystem.Web.ViewModels.Students
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class SpecialtyStudentViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }
        
        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Student, SpecialtyStudentViewModel>()
                .ForMember(s => s.FullName, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.MiddleName + " " + s.User.LastName));
        }
    }
}