using System;
using System.Collections.Generic;
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

        public ActionResult Index(int? sexId, int? sexWhoId, int? alcoholId, int? animalId, int? smokeId, int? activityId, int? cityId, IEnumerable<int> interests, int page = 1 )
        {
            if (_applicationManager.CurrentUser.ProfileId == null)
            {
                throw new ArgumentNullException();
            }
            var result = Reuqest(_contactService.GetContacts, "Index", (long) _applicationManager.CurrentUser.ProfileId,
                sexId, sexWhoId, alcoholId, animalId, smokeId, activityId, cityId, page, interests);
            return View(result);
        }

        private AvalibleFilters GetAvalibleFilters()
        {
            return new AvalibleFilters()
            {
                City = _directoryStorage.City.All(),
                Sex = _directoryStorage.Sex.All(),
                SexWho = _directoryStorage.Sex.All(),
                Alcohols = _directoryStorage.Alcohol.All(),
                Activity = _directoryStorage.Activity.All(),
                Intereses = _directoryStorage.Interes.All(),
                Animals = _directoryStorage.Animal.All(),
                Smokes = _directoryStorage.Smoke.All()
            };
        }

        private GetContactsResult Reuqest(
            Func<long, Pagination, ContactFilter, Tuple<List<Business.DataAccess.Public.Entities.Profile>, int>> reuqest,
            string name, long currentId, int? sexId, int? sexWhoId, int? alcoholId, int? animalId, int? smokeId, int? activityId,
            int? cityId, int page, IEnumerable<int> interests)
        {
            var filter = new ContactFilter(sexId, sexWhoId, alcoholId, animalId, smokeId, activityId, cityId, interests);
            var getContactsResult = reuqest(
                currentId,
                new Pagination()
                {
                    Page = page,
                    PageSize = GetPageSize()
                }, filter);

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = GetPageSize(),
                TotalItems = getContactsResult.Item2
            };

            return new GetContactsResult
            {
                PagingInfo = pagingInfo,
                Profiles = getContactsResult.Item1,
                AvalibleFilters = GetAvalibleFilters(),
                SelectedFilters = filter,
                Action = name
            };
        }

        private int GetPageSize()
        {
            return 9;
        }
    }
}