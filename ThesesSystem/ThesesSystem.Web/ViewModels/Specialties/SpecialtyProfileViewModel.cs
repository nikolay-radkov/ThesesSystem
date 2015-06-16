namespace ThesesSystem.Web.ViewModels.Specialties
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using System.Linq;
    using ThesesSystem.Web.ViewModels.Students;

    public class SpecialtyProfileViewModel : IMapFrom<Specialty>, IHaveCustomMappings
    {
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Брой студенти")]
        public int StudentsNumber { get; set; }

        [Display(Name = "Брой завършени дипломни работи")]
        public int ThesisCompleteNummber { get; set; }

        [Display(Name = "Брой дипломни работи в процес на разработка")]
        public int ThesisInProressNumber { get; set; }

        public ICollection<SpecialtyStudentViewModel> StudentsList { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Specialty, SpecialtyProfileViewModel>()
                  .ForMember(s => s.StudentsNumber, opt => opt.MapFrom(s => s.Students.Count))
                  .ForMember(f => f.ThesisCompleteNummber, opt => opt.MapFrom(st => st.Students.Sum(s => s.Theses.Where(t => t.IsComplete).Count())))
                  .ForMember(f => f.ThesisInProressNumber, opt => opt.MapFrom(st => st.Students.Sum(s => s.Theses.Where(t => !t.IsComplete).Count())));
        }
    }
}