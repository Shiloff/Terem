using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using System;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities.Identity;

namespace Project.WebUI.Models
{

  
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationManager : UserManager<ApplicationUserEntity>
    {
        public ApplicationManager(IUserStore<ApplicationUserEntity> store)
            : base(store)
        {
        }
        public static ApplicationManager Create(IdentityFactoryOptions<ApplicationManager> options,
                                            IOwinContext context)
        {
            var db = context.Get<EFDBContext>();
            var manager = new ApplicationManager(new UserStore<ApplicationUserEntity>(db));

            manager.EmailService = new Project.WebUI.Services.YandexEmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUserEntity>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }
            manager.UserValidator = new UserValidator<ApplicationUserEntity>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                //RequireDigit = false,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };
            return manager;
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole> 
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store)
            : base(store)
        {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
                                            IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<EFDBContext>());
            return new ApplicationRoleManager(roleStore);
        }
    }

   
}