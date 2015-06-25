namespace ThesesSystem.Web.ViewModels.Theses
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class ThesisTitleViewModel : IMapFrom<Thesis>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsComplete { get; set; }
    }
}