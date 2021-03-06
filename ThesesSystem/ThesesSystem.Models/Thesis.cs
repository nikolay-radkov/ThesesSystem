﻿namespace ThesesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ThesesSystem.Contracts;

    public class Thesis : DeletableEntity
    {
        private ICollection<ThesisPart> thesisParts;
        private ICollection<ThesisLog> thesisLogs;
        private ICollection<Version> versions;

        public Thesis()
        {
            this.thesisParts = new HashSet<ThesisPart>();
            this.thesisLogs = new HashSet<ThesisLog>();
            this.versions = new HashSet<Version>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime? FinishedAt { get; set; }

        public int? Pages { get; set; }

        public float? FinalEvaluation { get; set; }

        public bool IsComplete { get; set; }

        [ForeignKey("Supervisor")]
        [Required]
        public string SupervisorId { get; set; }

        [Required]
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Supervisor { get; set; }

        public virtual ICollection<ThesisPart> ThesisParts
        {
            get
            {
                return this.thesisParts;
            }

            set
            {
                this.thesisParts = value;
            }
        }

        public virtual ICollection<ThesisLog> ThesisLogs
        {
            get
            {
                return this.thesisLogs;
            }

            set
            {
                this.thesisLogs = value;
            }
        }

        public virtual ICollection<Version> Versions
        {
            get
            {
                return this.versions;
            }

            set
            {
                this.versions = value;
            }
        }

        public virtual Evaluation Evaluation { get; set; }
    }
}
