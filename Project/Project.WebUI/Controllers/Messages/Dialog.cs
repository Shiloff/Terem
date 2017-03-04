using System.Web.Mvc;
using Project.WebUI.Filters;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Messages
{
    [MyAuthorize]
    public partial class MessagesController : Controller
    {

        public ActionResult Dialog(long ProfileId = 0)
        {
            DialogMessagesViewResult result = new DialogMessagesViewResult();
            var profile = ProfileRepository.GetProfile(user.ProfileId);
            result.MyProfileId = profile.ProfileId;
            result.ToProfileId = ProfileId;
            result.Dialog = MessageRepository.GetDialog(profile.ProfileId, ProfileId, 100);
            return PartialView(result);
        }
       
	}
}