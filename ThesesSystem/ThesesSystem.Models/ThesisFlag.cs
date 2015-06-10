namespace ThesesSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum ThesisFlag
    {
        [Display(Name="Незапочната")]
        NotStarted,

        [Display(Name = "В прогрес")]
        InProgress,
        
        [Display(Name = "За проверка")]
        ForEvaluating,
        
        [Display(Name = "Завършена")]
        Complete
    }
}
