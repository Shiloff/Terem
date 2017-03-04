using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.WebUI.Filters;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Messages
{
    [MyAuthorize]
    public partial class MessagesController : Controller
    {
        
        public ActionResult UnreadMessagesShort(){
            var myProfile = ProfileRepository.GetProfile(user.ProfileId);
            UnreadMessagesShortResult result=new UnreadMessagesShortResult();

            var DialogProfile = MessageRepository.GetMyDialogs(myProfile.ProfileId);
            result.Dialogs = new List<DialogInfo>();
            foreach (var profile in DialogProfile)
            {
                DialogInfo dialogInfo = new Models.DialogInfo();
                dialogInfo.Profile = profile;
                dialogInfo.LastUpdate = MessageRepository.GetDialogLastMessageTime(profile.ProfileId, myProfile.ProfileId);
                dialogInfo.NewMessages = MessageRepository.GetDialogNewMessagesCount(profile.ProfileId, myProfile.ProfileId);
                var lastmessage = MessageRepository.GetDialog(profile.ProfileId, myProfile.ProfileId, 1).Where(m => m.ProfileIdTo == myProfile.ProfileId && ((m.Read == false) || (m.Read == null))).FirstOrDefault();
                if (lastmessage != null)
                {
                    dialogInfo.LastMessage = lastmessage.MessageText;
                    result.Dialogs.Add(dialogInfo);
                }
            }

            return PartialView(result);
        }
        
	}
}