namespace ThesesSystem.Web.ViewModels.ThesisThemes
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateThesisThemeViewModel : IMapFrom<ThesisTheme>
    {
        [Required(ErrorMessage="Трябва да попълните полето за заглавие")]
        [Display(Name="Заглавие")]
        public string Title { get; set; }
    }
}