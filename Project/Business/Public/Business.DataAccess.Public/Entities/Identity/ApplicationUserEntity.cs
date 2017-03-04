using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Business.DataAccess.Public.Entities.Identity
{
    public class ApplicationUserEntity : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync
                                   (UserManager<ApplicationUserEntity> manager)
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
}
