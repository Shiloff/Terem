using System;
using System.Web.Mvc;

namespace Project.WebUI.Controllers.Contacts
{
    public partial class ContactsController : Controller
    {
        public ActionResult Find(int? sexId, int? sexWhoId, int? alcoholId, int? animalId, int? smokeId, int? activityId, int page = 1)
        {
            if (_applicationManager.CurrentUser.ProfileId == null)
            {
                throw new ArgumentNullException();
            }
            var result = Reuqest(_contactService.FindContacts, "Find", (long) _applicationManager.CurrentUser.ProfileId,
                sexId, sexWhoId, alcoholId, animalId, smokeId, activityId, page);
            return View("Index", result);
        }
    }
}