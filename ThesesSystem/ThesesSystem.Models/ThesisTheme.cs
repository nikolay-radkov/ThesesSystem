namespace ThesesSystem.Models
{
    using ThesesSystem.Contracts;

    public class ThesisTheme : DeletableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsTaken { get; set; }

        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
