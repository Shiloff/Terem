using System.Collections.Generic;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using DataAccess.Public.Repository;

namespace Business.DataAccess.Public.Repository
{
    public interface IProfileRepository : IRepository<Profile, long>
    {
        Profile GetProfileWithDetails(long id);
        Profile GetShortProfile(long id);
        List<Profile> GetContacts(long profileId);
        void Update(Profile profile, ProfileUpdateMode mode = ProfileUpdateMode.None);
        void UpdateIntereses(long profileId, int[] interesesId);
    }
}
