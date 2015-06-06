namespace ThesesSystem.Web.ViewModels.Partial
{
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class StudentRegistrationViewModel : IMapFrom<Student>
    {
        [Display(Name = "Факултетен №")]
        public long FacultyNumber { get; set; }

        [Display(Name = "ОКС")]
        public Oks Oks { get; set; }

        [Display(Name = "Група")]
        public int SpecialtyGroup { get; set; }

        [Display(Name = "Специалност")]
        public string SpecialtyId { get; set; }

        [Display(Name = "Факултет")]
        public string FacultyId { get; set; }
    }
}