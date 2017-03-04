using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Business.DataAccess.Public.Repository.Specific.ProfileHelpers
{
    public interface IProfileFinderHelper
    {
        List<Profile> FindProfiles(Profile profile,IProfileRepository repo);
    }
}
