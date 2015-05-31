namespace ThesesSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ThesesSystem.Contracts;

    public class Specialty : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique=true)]
        [StringLength(20)]
        [MinLength(1)]
        public string Title { get; set; }

        public int FacultyId { get; set; }
    }
}
