﻿using System;
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

        public Tuple<List<Public.Entities.Profile>, int> GetContacts(long profileId, ContactFilter filter)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Profiles.GetContacts(profileId, filter);
            }
        }

        public Tuple<List<Public.Entities.Profile>, int> FindContacts(long myProfileId, ContactFilter filter)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Profiles.FindContacts(myProfileId, filter);
            }
        }
    }
}
