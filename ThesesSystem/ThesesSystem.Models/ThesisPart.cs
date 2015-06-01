namespace ThesesSystem.Models
{
    using ThesesSystem.Contracts;

    public class ThesisPart : DeletableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ThesisFlag Flag { get; set; }

        public int ThesisId { get; set; }

        public virtual Thesis Thesis { get; set; }
    }
}
