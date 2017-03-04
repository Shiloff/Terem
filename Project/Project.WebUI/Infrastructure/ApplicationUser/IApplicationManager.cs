
using System;
using Business.DataAccess.Public.Entities.Identity;
using Project.WebUI.Models;

namespace Project.WebUI.Infrastructure.ApplicationUser
{
    public interface IApplicationManager : IDisposable
    {
        ApplicationUserEntity CurrentUser { get; }

        ApplicationManager UserManager { get; set; }
    }
}
