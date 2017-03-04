using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class ProfileIndexResult
    {
        public Profile Profile { get; set; }
        public Profile Me { get; set; }
        public List<Profile> Contacts { get; set; }
        public int ContactsCount { get; set; }
        public bool MyProfile { get; set; }
    }
}