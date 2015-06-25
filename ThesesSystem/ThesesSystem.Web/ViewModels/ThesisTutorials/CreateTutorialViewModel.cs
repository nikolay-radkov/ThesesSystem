namespace ThesesSystem.Web.ViewModels.ThesisTutorials
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateTutorialViewModel : IMapFrom<ThesisTutorial>
    {
        [Required(ErrorMessage="Трябва да изберете файл")]
        [Display(Name="Документ")]
        public HttpPostedFileBase Archive { get; set; }
    }
}