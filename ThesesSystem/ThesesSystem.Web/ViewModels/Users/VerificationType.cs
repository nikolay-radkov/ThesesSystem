namespace ThesesSystem.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public enum VerificationType
    {
        [Display(Name="Студент")]
        Student,
        
        [Display(Name="Преподавател")]
        Teacher,

        [Display(Name="Бот")]
        Bot
    }
}