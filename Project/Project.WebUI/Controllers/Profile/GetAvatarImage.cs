using System.Linq;
using System.Web.Mvc;

namespace Project.WebUI.Controllers.Profile
{

    public partial class ProfileController : Controller
    {
        public FilePathResult GetAvatarImage(long profileId)
        {
            var profile = _profileService.GetShortProfile(profileId);
            return profile != null ? File(profile.GetImageAvatarLink, profile.ImageAvatarType ?? "jpeg") : null;
        }
    }
}