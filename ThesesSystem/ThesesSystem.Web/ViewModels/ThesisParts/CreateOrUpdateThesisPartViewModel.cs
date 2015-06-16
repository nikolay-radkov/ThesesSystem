namespace ThesesSystem.Web.ViewModels.ThesisParts
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateOrUpdateThesisPartViewModel : IMapFrom<ThesisPart>
    {
        [Display(Name = "Част")]
        [Required]
        public string Title { get; set; }

        public ThesisFlag Flag { get; set; }
    }
}