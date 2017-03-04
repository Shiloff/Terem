using System.Threading.Tasks;
using System.Web.Mvc;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Repository.Specific;
using Project.BusinessLogic.Services.ProfileService;
using Project.WebUI.Filters;
using Project.WebUI.Infrastructure.ApplicationUser;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Profile
{
    [MyAuthorize]
    public partial class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IApplicationManager _applicationManager;
        private readonly IProfileService _profileService;
        private readonly IDirectoryStorage _directoryStorage;

        public ProfileController(
            IProfileRepository profileRepository,
            IApplicationManager applicationManager,
            IProfileService profileService,
            IDirectoryStorage directoryStorage)
        {
            _applicationManager = applicationManager;
            _profileService = profileService;
            _profileRepository = profileRepository;
            _directoryStorage = directoryStorage;
        }

        public ActionResult Index(long? id)
        {
            var result = new ProfileIndexResult();
            var profileId = _applicationManager.CurrentUser.ProfileId;
            if ((id != null) && (profileId != id))
            {
                result.Profile = _profileService.GetProfileWithDetails((long)id);
                result.MyProfile = false;
            }
            else
            {
                result.Profile = _profileService.GetProfileWithDetails((long)profileId);
                result.MyProfile = true;
            }
            result.Me = _profileService.GetShortProfile((long)profileId);

            var contacts = _profileService.GetContacts(result.Profile.ProfileId, 10);
            result.Contacts = contacts.Profiles;
            result.ContactsCount = contacts.Count;
            if (result.Profile?.New != null && (result.Profile.New.Value != true))
            {
                return View(result);
            }
            return RedirectToAction("ProfileEdit");
        }

        private void UpdateProfileMEssage()
        {
            TempData["toastrMessage"] = $"Профиль {_applicationManager.CurrentUser.UserName} изменен";
            TempData["toastrType"] = "success";
        }
	}
}