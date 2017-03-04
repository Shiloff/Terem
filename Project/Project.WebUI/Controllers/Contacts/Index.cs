using System.Web;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities.Identity;
using Business.DataAccess.Public.Repository.Specific;
using Microsoft.AspNet.Identity;
using Project.WebUI.Models;
using Microsoft.AspNet.Identity.Owin;

namespace Project.WebUI.Controllers.Contacts
{
    public partial class ContactsController : Controller
    {
        IProfileRepository ProfileRepository;

        ApplicationUserEntity user
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationManager>().FindById(HttpContext.User.Identity.GetUserId());
            }
        }
        public ContactsController(IProfileRepository profileRepository)
        {
            ProfileRepository = profileRepository;
        }

        public ActionResult Index()
        {
            var result = new IndexContactsViewResult
            {
                Profiles = ProfileRepository.GetContacts((long) user.ProfileId)
            };
            return View(result);
        }
    }
}