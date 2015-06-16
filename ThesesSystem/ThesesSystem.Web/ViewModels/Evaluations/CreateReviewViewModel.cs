namespace ThesesSystem.Web.ViewModels.Evaluations
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Models;
    using System.Web;

    public class CreateReviewViewModel : IMapFrom<Evaluation>
    {
        public int Id { get; set; }

        [Range(2, 6, ErrorMessage="{0} трябва да бъде в интервала от 2 до 6")]
        [Display(Name = "Оценка")]
        [Required(ErrorMessage = "{0} е задължително поле")]
        public float Mark { get; set; }

        [Display(Name = "Файл")]
        [Required(ErrorMessage = "{0} е задължително поле")]
        public HttpPostedFileBase Archive { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Рецензент")]
        public string ReviewerId { get; set; }
    }
}