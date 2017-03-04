using System;
using System.Data.Entity.Migrations;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project.Domain.Migrations
{
    internal sealed partial class Configuration
    {
        private void SeedAdmin(EFDBContext context)
        {
            context.Roles.AddOrUpdate(m => m.Id,
              new IdentityRole() { Id = "1", Name = "Admin" },
              new IdentityRole() { Id = "2", Name = "User" });
            context.Users.AddOrUpdate(m => m.Id,
                new ApplicationUserEntity()
                {
                    Id = "a9b13910-e866-416a-b17c-422c3b6ba8b4",
                    RegistrationDate = new DateTime(2016, 1, 1),
                    LastLoginTime = new DateTime(2016, 1, 1),
                    ProfileId = 1,
                    Email = "racerbugaga@yandex.ru",
                    EmailConfirmed = true,
                    PasswordHash = "AP9XrVUtJVxZ+c5/LJ1cdUIrLoheIpWFGySQjghQJByjBBY9R8KoRXevT8ogh4HNDg==",
                    SecurityStamp = "72976120-ce49-433b-aaef-b48189c09bf3",
                    UserName = "Admin",
                    Roles =
                    {
                        new IdentityUserRole() { RoleId = "1", UserId = "a9b13910-e866-416a-b17c-422c3b6ba8b4" },
                        new IdentityUserRole() { RoleId = "2", UserId = "a9b13910-e866-416a-b17c-422c3b6ba8b4" },
                    }
                });
        }
    }
}
