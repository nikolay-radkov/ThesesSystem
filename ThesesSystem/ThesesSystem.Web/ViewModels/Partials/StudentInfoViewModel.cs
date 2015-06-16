namespace ThesesSystem.Web.ViewModels.Partials
{
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class StudentInfoViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
        [Display(Name="Факултетен №")]
        public long FacultyNumber { get; set; }

        [Display(Name="ОКС")]
        public Oks Oks { get; set; }

        [Display(Name="Група")]
        public int SpecialtyGroup { get; set; }

        [Display(Name="Специалност")]
        public string SpecialtyName { get; set; }

        [Display(Name = "Факултет")]
        public string FacultyName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Student, StudentInfoViewModel>()
               .ForMember(u => u.SpecialtyName, opt => opt.MapFrom(u => u.Specialty.Title))
               .ForMember(u => u.FacultyName, opt => opt.MapFrom(u => u.Specialty.Faculty.Title));
        }
    }
}