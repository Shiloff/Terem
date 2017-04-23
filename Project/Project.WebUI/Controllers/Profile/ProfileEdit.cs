using System.Web.Mvc;
using Business.DataAccess.Public.Repository.Specific;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Profile
{
    public partial class ProfileController : Controller
    {
        public ActionResult ProfileEdit()
        {
            var result = new ProfileEditResult
            {
                Profile = _profileService.GetShortProfile((long) _applicationManager.CurrentUser.ProfileId),
                ProfileSex = _directoryStorage.Sex.All(),
                ProfileActivity = _directoryStorage.Activity.All(),
                Cities = _directoryStorage.City.All()
            };
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileEdit(ProfileEditResult newProfile)
        {
            newProfile.ProfileSex = _directoryStorage.Sex.All();
            newProfile.ProfileActivity = _directoryStorage.Activity.All();
            newProfile.Cities = _directoryStorage.City.All();

            if (ModelState.IsValid)
            {
                newProfile.Profile.New = false;
                _profileService.UpdateProfile(newProfile.Profile, ProfileUpdateMode.Main);
                UpdateProfileMEssage();
            }
            return View(newProfile);
        }
    }
}