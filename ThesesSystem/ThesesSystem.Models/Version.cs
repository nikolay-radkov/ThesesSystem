namespace ThesesSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ThesesSystem.Contracts;

    public class Version : DeletableEntity
    {
        private ICollection<Comment> comments;

        public Version()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public string StoragePath { get; set; }

        public int ThesisId { get; set; }

        public virtual Thesis Thesis { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

    }
}
