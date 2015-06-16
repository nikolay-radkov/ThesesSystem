namespace ThesesSystem.Web.ViewModels.Theses
{
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Models;

    public class ThemeResultViewModel : IMapFrom<ThesisTheme>
    {
        public int Id { get; set; }

        public bool IsTaken { get; set; }

        public string Title { get; set; }
    }
}