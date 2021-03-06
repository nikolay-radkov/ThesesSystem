﻿namespace ThesesSystem.Web.ViewModels.ThesisParts
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateThesisPartViewModel : IMapFrom<ThesisPart>, IHaveCustomMappings
    {
        [Display(Name="Част")]
        [Required(ErrorMessage = "{0} е задължително поле")]
        public string Title { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<CreateThesisPartViewModel, ThesisPart>()
                .ForMember(u => u.Flag, opt => opt.MapFrom(u => ThesisFlag.NotStarted));
        }
    }
}