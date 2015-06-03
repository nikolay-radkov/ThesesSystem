namespace ThesesSystem.Web.ViewModels.Partial
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class TeacherInfoViewModel : IMapFrom<Teacher>
    {
        [Display(Name="Офис телефон")]
        public string OfficePhoneNumber { get; set; }

        [Display(Name="Кабинет")]
        public int Cabinet { get; set; }
    }
}