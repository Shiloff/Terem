using System;
using System.Collections.Generic;
using Business.DataAccess.Public.Entities;
namespace Project.WebUI.Models

{
    public class IndexContactsViewResult
    {
        public IEnumerable<Profile> Profiles { get; set; }
    }
}