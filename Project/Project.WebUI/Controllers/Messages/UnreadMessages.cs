using System.Web.Mvc;
using Project.WebUI.Filters;

namespace Project.WebUI.Controllers.Messages
{
    [MyAuthorize]
    public partial class MessagesController : Controller
    {
        
        public int? UnreadMessages(){
            var myProfile = ProfileRepository.GetProfile(user.ProfileId);
            int? result = MessageRepository.GetAllDialogNewMessagesCount(myProfile.ProfileId);
            return result != 0 ? result : null;
        }
        
	}
}