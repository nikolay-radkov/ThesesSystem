﻿namespace ThesesSystem.Web.ViewModels.Version
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Web.ViewModels.ThesisPart;

    public class CreateVersionViewModel : IMapFrom<Thesis>
    {
        public int Id { get; set; }

        [Required]
        public HttpPostedFileBase Archive { get; set; }

        public IList<CreateOrUpdateThesisPartViewModel> ThesisParts { get; set; }
    }
}