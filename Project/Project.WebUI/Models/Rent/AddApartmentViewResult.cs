using System;
using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class AddApartmentViewResult
    {
        public Apartment Apartment { get; set; }
        public List<ApartmentType> ApartmentTypes { get; set; }
    }
}