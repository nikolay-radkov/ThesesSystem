namespace ThesesSystem.Web.ViewModels.Filters
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum SortBy
    {
        [Display(Name = "Всичко")]
        [Description("Всичко")]
        All,

        [Display(Name = "Факултетен №")]
        [Description("Факултетен №")]
        FacultyNumber,

        [Display(Name = "ЕГН")]
        [Description("ЕГН")]
        EGN,

        [Display(Name = "Име на студента")]
        [Description("Име на студента")]
        StudentName,

        [Display(Name = "Име на ръководителя")]
        [Description("Име на ръководителя")]
        TeacherName,

        [Display(Name = "Специалност")]
        [Description("Специалност")]
        Specialty,

        [Display(Name = "Факултет")]
        [Description("Факултет")]
        Faculty,

        [Display(Name = "Дата на защита")]
        [Description("Дата на защита")]
        FinishedDate,

        [Display(Name = "Оценка")]
        [Description("Оценка")]
        FinalEvaluation,

        [Display(Name = "Заглавие")]
        [Description("Заглавие")]
        Thesis
    }
}