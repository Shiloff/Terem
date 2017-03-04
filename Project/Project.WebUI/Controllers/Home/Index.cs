using System.Collections.Generic;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using Project.WebUI.Filters;
using Project.WebUI.Infrastructure.ApplicationUser;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Home
{
    [MyAuthorize]
    public partial class HomeController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IApplicationManager _applicationUser;

        public HomeController(IProfileRepository profileRepository, IApartmentRepository apartmentRepository, IApplicationManager applicationUser)
        {
            _profileRepository = profileRepository;
            _apartmentRepository = apartmentRepository;
            _applicationUser = applicationUser;
        }

        public ActionResult Index()
        {
            IndexHomeViewResult result = new IndexHomeViewResult();
            result.Profile = _profileRepository.GetProfile(_applicationUser.CurrentUser.ProfileId);
            if (_applicationUser.CurrentUser?.ProfileId != null)
            {
                result.MyApartments = _apartmentRepository.GetMyApartments((int)_applicationUser.CurrentUser.ProfileId);
            }
            else
            {
                result.MyApartments = new List<Apartment>();
            }
            return View(result);
        }  
    }
}