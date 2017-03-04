
using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class ProfileActionsResult
    {
        public List<ProfileAction> Actions { get; set; }
        public Profile Profile { get; set; }
    }
}