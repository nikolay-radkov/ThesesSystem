namespace ThesesSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Contracts;

    public class Comment : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int VersionId { get; set; }

        public virtual Version Version { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
