namespace ThesesSystem.Web.ViewModels.ThesisTutorial
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateTutorialViewModel : IMapFrom<ThesisTutorial>
    {
        [Required]
        public HttpPostedFileBase Archive { get; set; }
    }
}