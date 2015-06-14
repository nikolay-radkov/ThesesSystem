namespace ThesesSystem.Web.ViewModels.Theses
{
    using System.Collections.Generic;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using System.Linq;

    public class DevThesisTimelineViewModel : IMapFrom<Thesis>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsComplete { get; set; }

        public bool HasEvaluation { get; set; }

        public ICollection<ThesisLogViewModel> ThesisLogs { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Thesis, DevThesisTimelineViewModel>()
             .ForMember(u => u.HasEvaluation, opt => opt.MapFrom(u => u.Evaluation != null));
        }
    }
}