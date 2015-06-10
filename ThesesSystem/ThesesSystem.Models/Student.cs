namespace ThesesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ThesesSystem.Contracts;

    public class Student : DeletableEntity
    {
        private ICollection<Thesis> theses;

        public Student()
        {
            this.Theses = new HashSet<Thesis>();
        }

        [Key, ForeignKey("User")]
        public string Id { get; set; }

        public long FacultyNumber { get; set; }

        public virtual Oks Oks { get; set; }


        public int SpecialtyId { get; set; }

        [Range(1, 100)]
        public int SpecialtyGroup { get; set; }

        public virtual Specialty Specialty { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Thesis> Theses
        {
            get
            {
                return this.theses;
            }

            set
            {
                this.theses = value;
            }
        }
    }
}
