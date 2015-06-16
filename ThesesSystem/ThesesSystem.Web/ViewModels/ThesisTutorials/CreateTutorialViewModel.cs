namespace ThesesSystem.Web.ViewModels.ThesisTutorials
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