namespace ThesesSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ThesesSystem.Contracts;

    public class Evaluation : DeletableEntity
    {
        [Key]
        [ForeignKey("Thesis")]
        public int Id { get; set; }

        [Range(2,6)]
        public float Mark { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        [MaxLength(10)]
        public string FileExtension { get; set; }

        [ForeignKey("Reviewer")]
        public string ReviewerId { get; set; }

        public virtual Teacher Reviewer { get; set; }

        public virtual Thesis Thesis { get; set; }
    }
}
