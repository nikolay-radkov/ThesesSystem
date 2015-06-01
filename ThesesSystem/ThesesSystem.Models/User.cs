namespace ThesesSystem.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ThesesSystem.Contracts;

    public class User : IdentityUser
    {
        private ICollection<Comment> comments;
        private ICollection<User> friends;
        private ICollection<Message> userMessages;
        private ICollection<Message> toUserMessages;

        public User()
        {
            this.comments = new HashSet<Comment>();
            this.friends = new HashSet<User>();
            this.userMessages = new HashSet<Message>();
            this.toUserMessages = new HashSet<Message>();
        }
        // TODO: Add user's fields
        //[Required]
        //[StringLength(20)]
        //[MinLength(2)]
        public string FirstName { get; set; }

        //[StringLength(20)]
        //[MinLength(2)]
        public string MiddleName { get; set; }

        //[Required]
        //[StringLength(20)]
        //[MinLength(2)] 
        public string LastName { get; set; }

        public long EGN { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return comments;
            }

            set
            {
                comments = value;
            }
        }
           
        public virtual ICollection<User> Friends
        {
            get
            {
                return friends;
            }

            set
            {
                friends = value;
            }
        }

        public virtual ICollection<Message> UserMessages
        {
            get
            {
                return userMessages;
            }

            set
            {
                userMessages = value;
            }
        }

        public virtual ICollection<Message> ToUserMessages
        {
            get
            {
                return toUserMessages;
            }

            set
            {
                toUserMessages = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
