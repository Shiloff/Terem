using System.Collections.Generic;
using System.Web;

namespace Project.WebUI.Models
{
    public class EditApartmentPhotoViewResult
    {
        public long ApartmentId { get; set; }       
        public List<HttpPostedFileBase> InputImages { get; set; } 
    }

   
}