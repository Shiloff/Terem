using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Business.DataAccess.Public.Repository.Specific.ProfileHelpers
{
    public interface IProfileContactsHelper
    {
        List<Profile> GetContacts(Profile profile,IProfileRepository repo);
    }
}
