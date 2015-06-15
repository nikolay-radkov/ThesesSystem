namespace ThesesSystem.Web.ViewModels.Theses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateFinalEvaluationViewModel : IMapFrom<Thesis>
    {
        public int Id { get; set; }
        
        [Required]
        [Range(2, 6)]
        [Display(Name="Крайна оценка")]
        public float? FinalEvaluation { get; set; }        
    }
}