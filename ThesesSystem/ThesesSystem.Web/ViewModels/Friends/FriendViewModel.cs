namespace ThesesSystem.Web.ViewModels.Friends
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class FriendViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<User, FriendViewModel>()
             .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.FirstName + " " + u.LastName));
        }
    }
}