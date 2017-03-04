using System.Web;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities.Identity;
using Business.DataAccess.Public.Repository.Specific;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project.WebUI.Models;


namespace Project.WebUI.Controllers
{
    public partial class RentController : Controller
    {
        IApartmentRepository ApartmentRepository;
        IProfileRepository ProfileRepository;
        ApplicationUserEntity user
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationManager>().FindById(HttpContext.User.Identity.GetUserId());
            }
        }
        public RentController(IApartmentRepository apartmentRepository,IProfileRepository profileRepository)
        {
            ApartmentRepository = apartmentRepository;
            ProfileRepository = profileRepository;
        }


        public ActionResult Index()
        {
            var result = new IndexRentViewResult();
            result.Apartments = ApartmentRepository.GetMyApartments((long)user.ProfileId);
                
            return View(result);
        }
	}
}