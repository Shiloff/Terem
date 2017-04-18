using System;
using System.Collections.Generic;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Services.Contact;
using DataAccess.Public.Repository;

namespace Business.DataAccess.Public.Repository
{
    public interface IProfileRepository : IRepository<Profile, long>
    {
        Profile GetProfileWithDetails(long id);
        Profile GetShortProfile(long id);
        Tuple<List<Profile>, int> GetContacts(long profileId, ContactFilter filter);
        Tuple<List<Profile>, int> FindContacts(long myProfileId, ContactFilter filter);
        void Update(Profile profile, ProfileUpdateMode mode = ProfileUpdateMode.None);
        void UpdateIntereses(long profileId, int[] interesesId);
    }
}
