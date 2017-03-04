using System.Linq;
using System.Web.Mvc;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Apartments
{
    public partial class ApartmentsController : Controller
    {

        public ActionResult Info(long? id)
        {
            InfoApartmentsViewResult result = new InfoApartmentsViewResult();
            var query = ApartmentRepository.GetApartment((long)id);           
            if (ApartmentRepository.IsMyApartment((long)id, (long)user.ProfileId))
            {                
                result.MyApartment = true;
            }
            else
            {
                ApartmentRepository.AddProfileVisit((long)id, (long)user.ProfileId);
            }
            result.Apartment = query;
            result.Apartment.ApartmentVisitors = result.Apartment.ApartmentVisitors.OrderByDescending(m => m.LastDate).ToList();
            result.Apartment.ApartmentComments = result.Apartment.ApartmentComments.OrderByDescending(m => m.Date).Take(3).ToList();
            result.Me = ProfileRepository.GetProfile(user.ProfileId);
            return View(result);
        }
	}
}