using System;
using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class LoadCommentsResult
    {
        public List<ProfileActionComment> ProfileActionComments { get; set; }
        public Profile Profile { get; set; }
        public ProfileAction Action { get; set; }
    }
}