namespace ThesesSystem.Web.Areas.Administration.ViewModels.Specialties
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class SpecialtyViewModel : IMapFrom<Specialty>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Факултет")]
        public string FacultyTitle { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Specialty, SpecialtyViewModel>()
            .ForMember(u => u.FacultyTitle, opt => opt.MapFrom(u => u.Faculty.Title));
        }
    }
}