namespace ThesesSystem.Web.ViewModels.Users
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class VerificationViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public long EGN { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<User, VerificationViewModel>()
               .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.FirstName + " " + u.MiddleName + " " + u.LastName));
        }
    }
}