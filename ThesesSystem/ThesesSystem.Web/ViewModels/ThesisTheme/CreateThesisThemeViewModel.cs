﻿namespace ThesesSystem.Web.ViewModels.ThesisTheme
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateThesisThemeViewModel : IMapFrom<ThesisTheme>
    {
        [Required]
        public string Title { get; set; }
    }
}