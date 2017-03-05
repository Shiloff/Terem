using System.Web.Mvc;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Services.Profile;
using Project.WebUI.Filters;
using Project.WebUI.Infrastructure.ApplicationUser;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Profile
{
    [MyAuthorize]
    public partial class ProfileController : Controller
    {
        private readonly IApplicationManager _applicationManager;
        private readonly IDirectoryStorage _directoryStorage;
        private readonly IProfileService _profileService;

        public ProfileController(
            IApplicationManager applicationManager,
            IProfileService profileService,
            IDirectoryStorage directoryStorage)
        {
            _applicationManager = applicationManager;
            _profileService = profileService;
            _directoryStorage = directoryStorage;
        }

        public ActionResult Index(long? id)
        {
            var result = new ProfileIndexResult();
            var profileId = _applicationManager.CurrentUser.ProfileId;
            if ((id != null) && (profileId != id))
            {
                result.Profile = _profileService.GetProfileWithDetails((long) id);
                result.MyProfile = false;
            }
            else
            {
                result.Profile = _profileService.GetProfileWithDetails((long) profileId);
                result.MyProfile = true;
            }
            result.Me = _profileService.GetShortProfile((long) profileId);

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

        private void AddCustomMessage(string message, string type)
        {
            TempData["toastrMessage"] = message;
            TempData["toastrType"] = type;
        }
    }
}