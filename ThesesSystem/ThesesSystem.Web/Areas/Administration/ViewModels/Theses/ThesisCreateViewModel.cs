namespace ThesesSystem.Web.Areas.Administration.ViewModels.Theses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisCreateViewModel : IMapFrom<Thesis>
    {
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Кратко описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Завършена на")]
        public DateTime? FinishedAt { get; set; }

        [Required]
        [Display(Name = "Брой страници")]
        public int? Pages { get; set; }

        [Required]
        [Display(Name = "Крайна оценка")]
        public float? FinalEvaluation { get; set; }

        [Required]
        public string SupervisorId { get; set; }

        [Required]
        public string ReviewerId { get; set; }

        [Required]
        [Display(Name = "Оценка от рецензия")]
        public float Mark { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Display(Name = "Дипломна работа")]
        public HttpPostedFileBase ThesisArchive { get; set; }

        [Display(Name = "Рецензия")]
        public HttpPostedFileBase ReviewArchive { get; set; }

    }
}