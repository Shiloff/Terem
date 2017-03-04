using System.Web.Mvc;
using Business.DataAccess.Public.Repository.Specific;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Contacts
{
    public partial class ContactsController : Controller
    {

        public ActionResult Find(int page = 1)
        {
            int PageSize = 9;
            FindContactsViewResult result = new FindContactsViewResult();
            var profile = ProfileRepository.GetProfile(user.ProfileId);
            PagingInfo PagingInfo = new PagingInfo();
            PagingInfo.CurrentPage = page;
            PagingInfo.ItemsPerPage = PageSize;
            PagingInfo.TotalItems = (int)ProfileRepository.GetFindProfilesCount(profile, new FindProfilesParams());

            result.PagingInfo = PagingInfo;
            result.Profiles = ProfileRepository.FindProfiles(profile,
                new FindProfilesParams
            {
                Take = PageSize,
                Skip = (page - 1) * PageSize
            });
            return View(result);
        }
	}
}