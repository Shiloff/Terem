using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class EditApartmentViewResult
    {
        public Apartment Apartment { get; set; }
        public List<ApartmentType> ApartmentTypes { get; set; }
        public List<ApartmentOption> ApartmentOptions { get; set; }
        public List<SelectedOptions> SelectedOptions { get; set; }
        public List<HttpPostedFileBase> InputImages { get; set; } 
    }

    public class SelectedOptions
    {
        public int ApartmentOptionId { get; set; }
        public bool Checked { get; set; }
    }
}