namespace ThesesSystem.Web.Areas.Administration.ViewModels.Faculties
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class FacultyViewModel : IMapFrom<Faculty>
    {
        public int Id { get; set; }

        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Създаден на")]
        public DateTime CreatedOn { get; set; }
    }
}