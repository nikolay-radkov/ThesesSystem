namespace ThesesSystem.Web.Areas.Administration.ViewModels.Specialties
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class SpecialtyCreateViewModel : IMapFrom<Specialty>
    {
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Факултет")]
        public int FacultyId { get; set; }
    }
}