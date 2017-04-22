using System;
using System.Collections.Generic;

namespace Business.DataAccess.Public.Services.Contact
{
    public interface IContactService
    {
        Tuple<List<Entities.Profile>, int> GetContacts(long profileId, Pagination pagination, ContactFilter filter);
        Tuple<List<Entities.Profile>, int> FindContacts(long myProfileId, Pagination pagination, ContactFilter filter);
    }
}