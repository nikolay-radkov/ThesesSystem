namespace ThesesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Contracts;

    public class Version : DeletableEntity
    {
        private ICollection<Comment> comments;

        public Version()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        public string StoragePath { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        [MaxLength(10)]
        public string FileExtension { get; set; }

        public int ThesisId { get; set; }

        public virtual Thesis Thesis { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }
    }
}
