using Business.DataAccess.Public.Repository.Specific;

namespace Business.DataAccess.Public.Services.Profile
{
    public interface IProfileService
    {
        Entities.Profile GetProfileWithDetails(long id);
        Entities.Profile GetShortProfile(long id);
        GetContactsResult GetContacts(long profileId, int count = 0);
        void UpdateProfile(Entities.Profile profile, ProfileUpdateMode mode, int[] selectedInteresesId = null);
    }
}
