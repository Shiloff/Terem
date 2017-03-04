using System.Collections.Generic;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Repository.Specific.ProfileHelpers;

namespace Business.DataAccess.Private.Repository.Specific.Helpers
{
    public class EFProfileContacts : IProfileContactsHelper
    {
        public List<Profile> GetContacts(Profile profile, IProfileRepository repo)
        {
            List<Profile> result=new List<Profile>();
           
            return result;

        }
    }
}
