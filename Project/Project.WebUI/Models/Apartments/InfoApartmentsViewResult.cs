using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class InfoApartmentsViewResult
    {
        public Apartment Apartment { get; set; }
        public bool MyApartment { get; set; }
        public Profile Me { get; set; }
    }
}