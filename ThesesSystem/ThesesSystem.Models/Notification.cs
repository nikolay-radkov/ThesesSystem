namespace ThesesSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Contracts;

    public class Notification : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string ForwardUrl { get; set; }

        public bool IsSeen { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
