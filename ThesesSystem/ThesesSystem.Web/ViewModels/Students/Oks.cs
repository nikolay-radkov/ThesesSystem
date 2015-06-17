namespace ThesesSystem.Web.ViewModels.Students
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum Oks
    {
        [Display(Name = "Бакалавър")]
        [Description("Бакалавър")]
        Bachelor,

        [Display(Name = "Магистър")]
        [Description("Магистър")]
        Master,

        [Display(Name = "Доктор")]
        [Description("Доктор")]
        Doctor
    }
}