using System;
using System.Web.Mvc;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Services.Contact;
using Project.WebUI.Infrastructure.ApplicationUser;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Contacts
{
    public partial class ContactsController : Controller
    {
        private readonly IApplicationManager _applicationManager;

        private readonly IContactService _contactService;

        public ContactsController(IApplicationManager applicationManager, IContactService contactService)
        {
            _applicationManager = applicationManager;
            _contactService = contactService;
        }

        public ActionResult Index(int page = 1)
        {
            if (_applicationManager.CurrentUser.ProfileId == null)
            {
                throw new ArgumentNullException();
            }

            var getContactsResult = _contactService
                .GetContacts((long) _applicationManager.CurrentUser.ProfileId,
                    new ContactFilter()
                    {
                        Page = page,
                        PageSize = GetPageSize()
                    });

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = GetPageSize(),
                TotalItems = getContactsResult.Item2
            };

            var result = new GetContactsResult
            {
                PagingInfo = pagingInfo,
                Profiles = getContactsResult.Item1
            };
            return View(result);
        }

        private int GetPageSize()
        {
            return 9;
        }
    }
}