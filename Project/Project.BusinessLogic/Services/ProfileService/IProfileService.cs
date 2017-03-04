using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;

namespace Project.BusinessLogic.Services.ProfileService
{
    public interface IProfileService
    {
        Profile GetProfileWithDetails(long id);
        Profile GetShortProfile(long id);
        GetContactsResult GetContacts(long profileId, int count = 0);
        void UpdateProfile(Profile profile, ProfileUpdateMode mode, int[] selectedInteresesId = null);
    }
}
