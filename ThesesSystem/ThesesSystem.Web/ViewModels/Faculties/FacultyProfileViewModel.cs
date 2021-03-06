﻿namespace ThesesSystem.Web.ViewModels.Faculties
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Web.ViewModels.Specialties;

    public class FacultyProfileViewModel : IMapFrom<Faculty>, IHaveCustomMappings
    {
        [Display(Name="Заглавие")]
        public string Title { get; set; }

        public ICollection<FacultySpecialtyViewModel> Specialties { get; set; }

        [Display(Name = "Брой студенти")]
        public int StudentsNumber { get; set; }

        [Display(Name = "Брой завършени дипломни работи")]
        public int ThesisCompleteNummber { get; set; }

        [Display(Name = "Брой дипломни работи в процес на разработка")]
        public int ThesisInProressNumber { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Faculty, FacultyProfileViewModel>()
                .ForMember(f => f.StudentsNumber, opt => opt.MapFrom(f => f.Specialties.Sum(s => s.Students.Count)))
                .ForMember(f => f.ThesisCompleteNummber, opt => opt.MapFrom(f => f.Specialties.Sum(st => st.Students.Sum(s => s.Theses.Where(t => t.IsComplete).Count()))))
                .ForMember(f => f.ThesisInProressNumber, opt => opt.MapFrom(f => f.Specialties.Sum(st => st.Students.Sum(s => s.Theses.Where(t => !t.IsComplete).Count()))));
        }
    }
}