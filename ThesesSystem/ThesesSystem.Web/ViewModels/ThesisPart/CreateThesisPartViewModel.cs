namespace ThesesSystem.Web.ViewModels.ThesisPart
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateThesisPartViewModel : IMapFrom<ThesisPart>, IHaveCustomMappings
    {
        [Display(Name="Част")]
        [Required]
        public string Title { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<CreateThesisPartViewModel, ThesisPart>()
                .ForMember(u => u.Flag, opt => opt.MapFrom(u => ThesisFlag.NotStarted));
        }
    }
}