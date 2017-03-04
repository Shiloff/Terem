using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project.Domain.Identity
{

  
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync
                                   (UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                                    DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("Banned", this.Banned.ToString()));

            return userIdentity;
            
        }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool? Banned { get; set; }
        public long? ProfileId { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
         public ApplicationDbContext()
            : base("EFDBContext")
        {
        }        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}