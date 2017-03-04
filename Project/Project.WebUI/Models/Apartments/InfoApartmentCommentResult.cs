using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class InfoApartmentCommentResult
    {
        public ApartmentComment Comment { get; set; }
        public Apartment Apartment { get; set; }
        public Profile Me { get; set; }
    }
}