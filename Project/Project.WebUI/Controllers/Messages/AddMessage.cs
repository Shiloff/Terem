using System;
using System.Web.Mvc;
using Project.WebUI.Filters;

namespace Project.WebUI.Controllers.Messages
{
    [MyAuthorize]
    public partial class MessagesController : Controller
    {
       
        [HttpPost]
        public void AddMessage(string message, int profileId = 0)
        {
            var profile = ProfileRepository.GetProfile(user.ProfileId);
            MessageRepository.AddMessage(profile.ProfileId, profileId, message, DateTime.Now);
        }
	}
}