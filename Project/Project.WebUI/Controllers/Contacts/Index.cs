using System;
using System.Linq;
using System.Web.Mvc;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Services.Contact;
using Project.WebUI.Infrastructure.ApplicationUser;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Contacts
{
    public partial class ContactsController : Controller
    {
        private readonly IApplicationManager _applicationManager;
        private readonly IContactService _contactService;
        private readonly IDirectoryStorage _directoryStorage;

        public ContactsController(IApplicationManager applicationManager, IContactService contactService, IDirectoryStorage directoryStorage)
        {
            _applicationManager = applicationManager;
            _contactService = contactService;
            _directoryStorage = directoryStorage;
        }

        public ActionResult Index(int? sexId, int? alcoholId, int? animalId, int? smokeId, int? activityId, int page = 1 )
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
                Profiles = getContactsResult.Item1,
                AvalibleFilters = GetAvalibleFilters(),
                SelectedFilters = new SelectedFilters(sexId, alcoholId, animalId, smokeId, activityId)
            };
            return View(result);
        }

        private AvalibleFilters GetAvalibleFilters()
        {
            return new AvalibleFilters()
            {
                Sex = _directoryStorage.Sex.All(),
                Alcohols = _directoryStorage.Alcohol.All(),
                Activity = _directoryStorage.Activity.All(),
                Intereses = _directoryStorage.Interes.All(),
                Animals = _directoryStorage.Animal.All(),
                Smokes = _directoryStorage.Smoke.All()
            };
        }

        private int GetPageSize()
        {
            return 1;
        }
    }
}