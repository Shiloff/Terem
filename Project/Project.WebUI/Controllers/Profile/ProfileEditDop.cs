using System.Web.Mvc;
using Business.DataAccess.Public.Repository.Specific;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Profile
{

    public partial class ProfileController : Controller
    {
        public ActionResult ProfileEditDop()
        {
            var result = new ProfileEditResult
            {
                Profile = _profileService.GetProfileWithDetails((long) _applicationManager.CurrentUser.ProfileId),
            };
            result = LoadEditDopData(result);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileEditDop(ProfileEditResult newProfile)
        {
            newProfile = LoadEditDopData(newProfile);

            if (ModelState.IsValid)
            {
                newProfile.Profile.New = false;
                _profileService.UpdateProfile(newProfile.Profile, ProfileUpdateMode.Dop, newProfile.SelectedInterests);
                newProfile.Profile =
                    _profileService.GetProfileWithDetails(newProfile.Profile.ProfileId);

                UpdateProfileMEssage();
            }
            return View(newProfile);
        }

        private ProfileEditResult LoadEditDopData(ProfileEditResult data)
        {
            data.ProfileSex = _directoryStorage.Sex.All();
            data.ProfileAlcohol = _directoryStorage.Alcohol.All();
            data.ProfileSmoking = _directoryStorage.Smoke.All();
            data.ProfileAnimals = _directoryStorage.Animal.All();
            data.ProfileActivity = _directoryStorage.Activity.All();
            data.Interests = _directoryStorage.Interes.All();
            return data;
        }
	}
}