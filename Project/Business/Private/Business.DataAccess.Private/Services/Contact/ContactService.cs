using System;
using System.Collections.Generic;
using Business.DataAccess.Public.Services.Contact;
using Business.DataAccess.Public.UnitOfWork.Factory;

namespace Business.DataAccess.Private.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public ContactService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Tuple<List<Public.Entities.Profile>, int> GetContacts(long profileId, Pagination pagination, ContactFilter filter)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Profiles.GetContacts(profileId, pagination, filter);
            }
        }

        public Tuple<List<Public.Entities.Profile>, int> FindContacts(long myProfileId, Pagination pagination, ContactFilter filter)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Profiles.FindContacts(myProfileId, pagination, filter);
            }
        }
    }
}
