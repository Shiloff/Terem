using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Repository.Specific.ProfileHelpers;

namespace Business.DataAccess.Private.Repository.Specific.Helpers
{
    public class EFProfileFinder : IProfileFinderHelper
    {
        public List<Profile> FindProfiles(Profile profile,IProfileRepository repo)
        {
            List<Profile> result;
            result = repo.Profiles
                .Where(m => m.New == false)
                .Where(m => m.ProfileId != profile.ProfileId)
                .ToList();
            return result;
           
        }
    }
}
