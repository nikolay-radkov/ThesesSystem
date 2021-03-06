﻿namespace ThesesSystem.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        private ICollection<Comment> comments;
        private ICollection<User> friends;
        private ICollection<Message> userMessages;
        private ICollection<Message> toUserMessages;
        private ICollection<Notification> notifications;
        private ICollection<ThesisLog> thesisLogs;

        public User()
        {
            this.comments = new HashSet<Comment>();
            this.friends = new HashSet<User>();
            this.userMessages = new HashSet<Message>();
            this.toUserMessages = new HashSet<Message>();
            this.notifications = new HashSet<Notification>();
            this.thesisLogs = new HashSet<ThesisLog>();
        }

        [Required]
        [StringLength(20)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [StringLength(20)]
        [MinLength(2)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(20)]
        [MinLength(2)] 
        public string LastName { get; set; }

        public long EGN { get; set; }

        public bool IsVerified { get; set; }
        
        public bool IsRegistrationCompleted { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }
           
        public virtual ICollection<User> Friends
        {
            get
            {
                return this.friends;
            }

            set
            {
                this.friends = value;
            }
        }

        public virtual ICollection<Message> UserMessages
        {
            get
            {
                return this.userMessages;
            }

            set
            {
                this.userMessages = value;
            }
        }

        public virtual ICollection<Message> ToUserMessages
        {
            get
            {
                return this.toUserMessages;
            }

            set
            {
                this.toUserMessages = value;
            }
        }

        public virtual ICollection<Notification> Notifications
        {
            get
            {
                return this.notifications;
            }

            set
            {
                this.notifications = value;
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


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
