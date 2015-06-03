namespace ThesesSystem.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class UserProfileViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name = "Име")]
        public string FullName { get; set; }

        [Display(Name = "ЕГН")]
        public long EGN { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        public bool IsVerified { get; set; }

        public bool IsFriend { get; set; }

        public bool IsStudent { get; set; }

        [Display(Name="Е-поща")]
        public string Email { get; set; }
        
        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<User, UserProfileViewModel>()
               .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.FirstName + " " + u.MiddleName + " " + u.LastName))
               .ForMember(u => u.IsStudent, opt => opt.MapFrom(u => u.Student != null));
        }
    }
}