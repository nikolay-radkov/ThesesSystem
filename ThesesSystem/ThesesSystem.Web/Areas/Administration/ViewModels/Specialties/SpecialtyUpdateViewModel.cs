namespace ThesesSystem.Web.Areas.Administration.ViewModels.Specialties
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class SpecialtyUpdateViewModel : IMapFrom<Specialty>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Факултет")]
        public int FacultyId { get; set; }

        [Display(Name = "Факултет")]
        public string FacultyTitle { get; set; }

        [Display(Name = "Създадена на")]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Specialty, SpecialtyUpdateViewModel>()
                .ForMember(s => s.FacultyTitle, opt => opt.MapFrom(s => s.Faculty.Title));
        }
    }
}