namespace ThesesSystem.Web.ViewModels.Versions
{
    using System.Collections.Generic;
    using ThesesSystem.Web.Infrastructure.Mapping;
    using ThesesSystem.Web.ViewModels.Comments;

    public class VersionProfileViewModel : IMapFrom<ThesesSystem.Models.Version>
    {
        public int Id { get; set; }

        public int ThesisId { get; set; }

        public string FileName { get; set; }

        public string StoragePath { get; set; }

        public ICollection<CommentProfileViewModel> Comments { get; set; }
    }
}