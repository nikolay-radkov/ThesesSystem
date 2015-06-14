namespace ThesesSystem.Web.ViewModels.Version
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class VersionProfileViewModel : IMapFrom<ThesesSystem.Models.Version>
    {
        public string StoragePath { get; set; }
    }
}