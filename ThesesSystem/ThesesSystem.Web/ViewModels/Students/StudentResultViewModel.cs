namespace ThesesSystem.Web.ViewModels.Students
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class StudentResultViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Student, StudentResultViewModel>()
             .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.User.FirstName + " " + u.User.MiddleName + " " + u.User.LastName))
            .ForMember(u => u.Email, opt => opt.MapFrom(u => u.User.Email));
        }
    }
}