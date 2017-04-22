using System;
using System.Web.Mvc;
using Business.DataAccess.Public.Services.Contact;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Contacts
{
    public partial class ContactsController : Controller
    {
        public ActionResult Find(int page = 1)
        {
            if (_applicationManager.CurrentUser.ProfileId == null)
            {
                throw new ArgumentNullException();
            }

            var findContactsResult = _contactService
                .FindContacts((long) _applicationManager.CurrentUser.ProfileId,
                    new Pagination()
                    {
                        Page = page,
                        PageSize = GetPageSize()
                    }, new ContactFilter());

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = GetPageSize(),
                TotalItems = findContactsResult.Item2
            };

            var result = new FindContactsResult
            {
                PagingInfo = pagingInfo,
                Profiles = findContactsResult.Item1
            };
            return View(result);
        }
    }
}