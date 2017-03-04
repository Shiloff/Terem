using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Business.DataAccess.Public.Entities.Identity;

namespace Project.WebUI.Models
{
    public class AdminUserEditViewResult
    {
        public ApplicationUserEntity User { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public List<SelectedRole> SelectedRoles { get; set; }
        [Display(Name = "Бан")]
        public bool Banned { get; set; }
        public string returnUrl { get; set; }
    }
    public class SelectedRole
    {
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}