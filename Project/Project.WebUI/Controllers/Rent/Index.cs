using System.Web;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities.Identity;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Services.Profile;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project.WebUI.Models;


namespace Project.WebUI.Controllers
{
    public partial class RentController : Controller
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileService _profileService;
        ApplicationUserEntity user
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationManager>().FindById(HttpContext.User.Identity.GetUserId());
            }
        }
        public RentController(IApartmentRepository apartmentRepository,IProfileRepository profileRepository, IProfileService profileService)
        {
            _apartmentRepository = apartmentRepository;
            _profileRepository = profileRepository;
            _profileService = profileService;
        }


        public ActionResult Index()
        {
            var result = new IndexRentViewResult();
            result.Apartments = _apartmentRepository.GetMyApartments((long)user.ProfileId);
                
            return View(result);
        }
	}
}