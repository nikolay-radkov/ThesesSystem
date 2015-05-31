namespace ThesesSystem.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        // TODO: Add user's fields
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
