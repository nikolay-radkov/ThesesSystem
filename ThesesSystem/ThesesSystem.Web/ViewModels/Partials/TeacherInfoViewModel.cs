namespace ThesesSystem.Web.ViewModels.Partials
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Web.ViewModels.Theses;

    public class TeacherInfoViewModel : IMapFrom<Teacher>
    {
        [Display(Name="Офис телефон")]
        public string OfficePhoneNumber { get; set; }

        [Display(Name="Кабинет")]
        public int Cabinet { get; set; }

        public ICollection<ThesisTitleViewModel> Theses { get; set; }
    }
}