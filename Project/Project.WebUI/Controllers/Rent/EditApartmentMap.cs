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
            var result = new EditApartmentViewResult
            {
                Apartment = _apartmentRepository.GetApartment(id),
                ApartmentOptions = _apartmentRepository.GetApartmentOptions()
            };
            if (result.Apartment == null)
            {
                result.Apartment = new Apartment
                {
                    ApartmentOptions = new List<ApartmentOption>(),
                    ApartmentPhotos = new List<ApartmentPhoto>()
                };
            }
            result.ApartmentTypes = _apartmentRepository.ApartmentTypes.ToList();
            return View(result);
        }
        [HttpPost]
        public void EditApartmentMap(EditApartmentMapData value)
        {
            var apartment= _apartmentRepository.GetApartment(value.ApartmentId);
            _apartmentRepository.UpdateCoords(apartment, new ApartmentCoords { Latitude = value.Latitude, Longitude = value.Longitude });

        }
	}
}