namespace ThesesSystem.Web.ViewModels.Teachers
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class TeacherResultViewModel : IMapFrom<Teacher>, IHaveCustomMappings
    {
        public string Id { get; set; }
        
        public string FullName { get; set; }

        public string Email { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Teacher, TeacherResultViewModel>()
             .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.User.FirstName + " " + u.User.MiddleName + " " + u.User.LastName))
            .ForMember(u => u.Email, opt => opt.MapFrom(u => u.User.Email));
        }
    }
}