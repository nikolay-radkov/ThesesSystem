namespace ThesesSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Contracts;

    public class ThesisTutorial : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
