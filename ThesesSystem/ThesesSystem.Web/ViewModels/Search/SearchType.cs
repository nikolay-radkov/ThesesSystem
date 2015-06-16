using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ThesesSystem.Web.ViewModels.Search
{
    public enum SearchType
    {
        [Display(Name="Всичко")]
        [Description("Всичко")]
        Everywhere,

        [Display(Name = "Студенти")]
        [Description("Студенти")]
        Students,

        [Display(Name = "Преподаватели")]
        [Description("Преподаватели")]
        Teachers,

        [Display(Name = "Факултети")]
        [Description("Факултети")]
        Faculties,

        [Display(Name = "Специалности")]
        [Description("Специалности")]
        Specialties,

        [Display(Name = "Архив")]
        [Description("Архив")]
        Archive,

        [Display(Name = "Теми")]
        [Description("Теми")]
        Themes
    }
}