namespace ThesesSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Contracts;

    public class ThesisTheme : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsTaken { get; set; }

        [Required]
        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
