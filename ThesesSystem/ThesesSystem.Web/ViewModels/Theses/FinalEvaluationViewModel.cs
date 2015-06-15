namespace ThesesSystem.Web.ViewModels.Theses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class FinalEvaluationViewModel : IMapFrom<Thesis>
    {
        [Display(Name = "Крайна оценка")]
        public float? FinalEvaluation { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? FinishedAt { get; set; }

        [Display(Name = "Страници")]
        public int? Pages { get; set; }
    }
}