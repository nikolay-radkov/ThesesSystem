namespace ThesesSystem.Models
{
    using System.Collections.Generic;
    using ThesesSystem.Contracts;

    public class Comment : DeletableEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int VersionId { get; set; }

        public virtual Version Version { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
