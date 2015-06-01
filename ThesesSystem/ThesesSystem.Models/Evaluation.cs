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

        public float Mark { get; set; }

        public string FilePath { get; set; }

        [ForeignKey("Reviewer")]
        public string ReviewerId { get; set; }

        public virtual Teacher Reviewer { get; set; }

        public virtual Thesis Thesis { get; set; }
    }
}
