namespace ThesesSystem.Web.ViewModels.Filters
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum SortType
    {
        [Display(Name = "Възходящо")]
        [Description("Възходящо")]
        Ascending,

        [Display(Name = "Низходящо")]
        [Description("Низходящо")]
        Descending
    }
}