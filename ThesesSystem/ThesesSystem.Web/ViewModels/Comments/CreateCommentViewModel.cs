namespace ThesesSystem.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Models;
    using ThesesSystem.Web.Infrastructure.Mapping;

    public class CreateCommentViewModel : IMapFrom<Comment>
    {
        [Required]
        public string Text { get; set; }

        public int VersionId { get; set; }
    }
}