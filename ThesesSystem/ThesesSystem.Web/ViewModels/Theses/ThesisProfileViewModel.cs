namespace ThesesSystem.Web.ViewModels.Theses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisProfileViewModel : IMapFrom<Thesis>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public string SupervisorId { get; set; }

        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Факултет")]
        public string Faculty { get; set; }

        [Display(Name = "Специалност")]
        public string Specialty { get; set; }


        [Display(Name = "Защитена на")]
        public DateTime? FinishedAt { get; set; }

        [Display(Name = "Страници")]
        public int? Pages { get; set; }

        [Display(Name = "Оценка")]
        public float? FinalEvaluation { get; set; }

        [Display(Name = "Научен ръководител")]
        public string SupervisorName { get; set; }

        [Display(Name = "Студент")]
        public string StudentName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Thesis, ThesisProfileViewModel>()
               .ForMember(u => u.StudentName, opt => opt.MapFrom(u => u.Student.User.FirstName + " " + u.Student.User.MiddleName + " " + u.Student.User.LastName))
               .ForMember(u => u.SupervisorName, opt => opt.MapFrom(u => u.Supervisor.User.FirstName + " " + u.Supervisor.User.MiddleName + " " + u.Supervisor.User.LastName))
               .ForMember(u => u.Faculty, opt => opt.MapFrom(u => u.Student.Specialty.Faculty.Title))
               .ForMember(u => u.Specialty, opt => opt.MapFrom(u => u.Student.Specialty.Title));
        }
    }
}