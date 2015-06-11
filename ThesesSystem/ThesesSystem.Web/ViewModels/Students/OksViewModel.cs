namespace ThesesSystem.Web.ViewModels.Students
{
    using System.ComponentModel.DataAnnotations;

    public enum Oks
    {
        [Display(Name = "Бакалавър")]
        Bachelor,

        [Display(Name = "Магистър")]
        Master,

        [Display(Name = "Доктор")]
        Doctor
    }
}