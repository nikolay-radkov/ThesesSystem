namespace ThesesSystem.Models
{
    using ThesesSystem.Contracts;

    public class Message : DeletableEntity
    {
        public int Id { get; set; }
        
        public string Text { get; set; }
        
        public string FromUserId { get; set; }

        public string ToUserId { get; set; }

        public virtual User FromUser { get; set; }

        public virtual User ToUser { get; set; }
    }
}
