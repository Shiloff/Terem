using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class IndexHomeViewResult
    {
        public Profile Profile { get; set; }
        public List<Apartment> MyApartments { get; set; }
    }
}