using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class AddActionResult
    {
        public ProfileAction ProfileAction{ get; set; }
        public List<ProfileActionComment> ProfileActionComments { get; set; }
        public Profile Profile { get; set; }
    }
}