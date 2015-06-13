namespace ThesesSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Contracts;

    public class ThesisLog : DeletableEntity
    {
        public int Id { get; set; }

        public int ThesisId { get; set; }

        [Required]
        public string ForwardUrl { get; set; }

        public LogType LogType { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Thesis Thesis { get; set; }
    }
}
