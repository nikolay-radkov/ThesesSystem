namespace ThesesSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Contracts;

    public class ThesisPart : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public ThesisFlag Flag { get; set; }

        public int ThesisId { get; set; }

        public virtual Thesis Thesis { get; set; }
    }
}
