using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Public.Entities;

namespace Business.DataAccess.Public.Repository.Specific
{
    public enum ProfileUpdateMode
    {
        Main,
        Dop,
        Avatar,
        None
    }
    public struct FindProfilesParams
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
    public interface IProfileRepository
    {
        Profile GetProfile(long? profileId);
        bool IsProfileExists(long? profileId);
        List<Profile> FindProfiles(Profile profile, FindProfilesParams param);
        long GetFindProfilesCount(Profile profile, FindProfilesParams param);
        List<Profile> GetContacts(long profileId, int count = 0);
    }
}
