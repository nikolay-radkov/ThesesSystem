namespace ThesesSystem.Web.ViewModels.Teachers
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class SupervisorDropDownListITemViewModel : IMapFrom<Teacher>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Display(Name="Ръководител")]
        public string FullName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Teacher, SupervisorDropDownListITemViewModel>()
               .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.User.FirstName + " " + u.User.MiddleName + " " + u.User.LastName));
        }
    }
}