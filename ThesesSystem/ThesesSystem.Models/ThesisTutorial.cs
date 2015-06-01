namespace ThesesSystem.Models
{
    using ThesesSystem.Contracts;

    public class ThesisTutorial : DeletableEntity
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
