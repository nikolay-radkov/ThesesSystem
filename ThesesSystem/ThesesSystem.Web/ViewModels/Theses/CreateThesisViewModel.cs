namespace ThesesSystem.Web.ViewModels.Theses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Web.ViewModels.ThesisParts;

    public class CreateThesisViewModel : IMapFrom<Thesis>
    {
        public CreateThesisViewModel()
        {
            this.ThesisParts = new HashSet<CreateThesisPartViewModel>();
        }

        [Required(ErrorMessage = "{0} е задължително поле")]
        public string SupervisorId { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required(ErrorMessage="{0} е задължително поле")]
        [Display(Name = "Кратко описание")]
        public string Description { get; set; }

        public ICollection<CreateThesisPartViewModel> ThesisParts { get; set; }
    }
}