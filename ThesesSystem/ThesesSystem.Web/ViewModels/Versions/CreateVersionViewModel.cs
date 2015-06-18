namespace ThesesSystem.Web.ViewModels.Versions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Web.ViewModels.ThesisParts;

    public class CreateVersionViewModel : IMapFrom<Thesis>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Архив")]
        public HttpPostedFileBase Archive { get; set; }

        [Required]
        [Display(Name="Страници")]
        public int? Pages { get; set; }

        public IList<CreateOrUpdateThesisPartViewModel> ThesisParts { get; set; }
    }
}