using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers
{
    public partial class RentController : Controller
    {

        public ActionResult EditApartmentMap(long id = 0)
        {
            EditApartmentViewResult result = new EditApartmentViewResult();
            result.Apartment = ApartmentRepository.GetApartment(id);
            result.ApartmentOptions = ApartmentRepository.GetApartmentOptions();
            if (result.Apartment == null)
            {
                result.Apartment = new Apartment();
                result.Apartment.ApartmentOptions = new List<ApartmentOption>();
                result.Apartment.ApartmentPhotos = new List<ApartmentPhoto>();
            }
            result.ApartmentTypes = ApartmentRepository.ApartmentTypes.ToList();
            return View(result);
        }
        [HttpPost]
        public void EditApartmentMap(EditApartmentMapData value)
        {
            Apartment apartment= ApartmentRepository.GetApartment(value.ApartmentId);
            ApartmentRepository.UpdateCoords(apartment, new ApartmentCoords { Latitude = value.Latitude, Longitude = value.Longitude });

        }
	}
}