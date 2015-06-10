namespace ThesesSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using ThesesSystem.Contracts;

    public class Message : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsSeen { get; set; }

        [Required]
        public string FromUserId { get; set; }

        [Required]
        public string ToUserId { get; set; }

        public virtual User FromUser { get; set; }

        public virtual User ToUser { get; set; }
    }
}
