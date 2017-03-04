using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities.Identity;
using Business.DataAccess.Public.Repository.Specific;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project.WebUI.Filters;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Messages
{
    [MyAuthorize]
    public partial class MessagesController : Controller
    {
        IProfileRepository ProfileRepository;
        IMessageRepository MessageRepository;
        ApplicationUserEntity user
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationManager>().FindById(HttpContext.User.Identity.GetUserId());
            }
        }
        public MessagesController(IProfileRepository profileRepository, IMessageRepository messageRepository)
        {
            ProfileRepository = profileRepository;
            MessageRepository = messageRepository;
        }

        public ActionResult Index(long? openedChat)
        {
            
            IndexMessagesViewResult result = new IndexMessagesViewResult();

            var myProfile = ProfileRepository.GetProfile(user.ProfileId);
            result.Profile = myProfile;
            var DialogProfile = MessageRepository.GetMyDialogs(myProfile.ProfileId);
            result.Dialogs = new List<DialogInfo>();
            foreach (var profile in DialogProfile)
            {
                DialogInfo dialogInfo = new Models.DialogInfo();
                dialogInfo.Profile = profile;
                dialogInfo.LastUpdate = MessageRepository.GetDialogLastMessageTime(profile.ProfileId, myProfile.ProfileId);
                dialogInfo.NewMessages = MessageRepository.GetDialogNewMessagesCount(profile.ProfileId, myProfile.ProfileId);
                result.Dialogs.Add(dialogInfo);
            }
            var lastDialog = DialogProfile.FirstOrDefault();
            result.openedChat = (openedChat != null && openedChat != myProfile.ProfileId && ProfileRepository.IsProfileExists(openedChat) ? (long)openedChat : lastDialog != null ? lastDialog.ProfileId : 0);
            return View(result);
        }
        
	}
}