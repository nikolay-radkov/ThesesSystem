namespace ThesesSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ThesesSystem.Contracts;

    public class Faculty : DeletableEntity
    {
        private ICollection<Specialty> specialties;

        public Faculty()
        {
            this.specialties = new HashSet<Specialty>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string Title { get; set; }

        public virtual ICollection<Specialty> Specialties
        {
            get
            {
                return this.specialties;
            }

            set
            {
                this.specialties = value;
            }
        }
    }
}
