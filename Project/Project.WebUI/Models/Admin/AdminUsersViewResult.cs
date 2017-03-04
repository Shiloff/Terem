using System.Collections.Generic;
using Business.DataAccess.Public.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project.WebUI.Models
{
    public class AdminUsersViewResult
    {
        public ICollection<ApplicationUserEntity> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string Sorted { get; set; }
        public string UserNameFilter { get; set; }
        public string RoleFilter { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}