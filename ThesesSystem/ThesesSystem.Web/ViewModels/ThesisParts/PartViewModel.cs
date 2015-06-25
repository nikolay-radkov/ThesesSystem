namespace ThesesSystem.Web.ViewModels.ThesisParts
{
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class PartViewModel: IMapFrom<ThesisPart>
    {
        public string Title { get; set; }
    }
}