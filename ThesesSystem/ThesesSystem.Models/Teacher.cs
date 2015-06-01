namespace ThesesSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ThesesSystem.Contracts;

    public class Teacher : DeletableEntity
    {
        private ICollection<ThesisTheme> thesisThemes;
        private ICollection<ThesisTutorial> thesisTutorials;
        private ICollection<Thesis> theses;
        private ICollection<Evaluation> evaluations;

        public Teacher()
        {
            this.thesisThemes = new HashSet<ThesisTheme>();
            this.thesisTutorials = new HashSet<ThesisTutorial>();
            this.theses = new HashSet<Thesis>();
            this.evaluations = new HashSet<Evaluation>();
        }

        [Key, ForeignKey("User")]
        public string Id { get; set; }

        public string OfficePhoneNumber { get; set; }

        public int Cabinet { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<ThesisTutorial> ThesisTutorials
        {
            get
            {
                return thesisTutorials;
            }

            set
            {
                thesisTutorials = value;
            }
        }

        public virtual ICollection<ThesisTheme> ThesisThemes
        {
            get
            {
                return thesisThemes;
            }

            set
            {
                thesisThemes = value;
            }
        }

        public virtual ICollection<Thesis> Theses
        {
            get
            {
                return theses;
            }

            set
            {
                theses = value;
            }
        }

        public virtual ICollection<Evaluation> Evaluations
        {
            get
            {
                return evaluations;
            }

            set
            {
                evaluations = value;
            }
        }
    }
}
