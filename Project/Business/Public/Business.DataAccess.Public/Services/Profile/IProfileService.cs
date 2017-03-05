using System.Collections.Generic;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;

namespace Business.DataAccess.Public.Services.Profile
{
    public interface IProfileService
    {
        void AddProfile(Entities.Profile profile);
        Entities.Profile GetProfileWithDetails(long id);
        Entities.Profile GetShortProfile(long id);
        GetContactsResult GetContacts(long profileId, int count = 0);
        void UpdateProfile(Entities.Profile profile, ProfileUpdateMode mode, int[] selectedInteresesId = null);
        List<ProfileAction> GetProfileActions(long id);
        ProfileAction GetProfileAction(long id);
        ProfileAction AddProfileAction(ProfileAction addAction, long profileId = 0);
        void RemoveProfileAction(long id, long profileId);
        void AddActionLike(long id, long profileId);
        void RemoveActionLike(long id, long profileId);
        long AddProfileActionComment(ProfileActionComment comment);
        ProfileActionComment GetProfileActionsComment(long id);
        void RemoveProfileActionsComment(long id, long profileId);
        void AddProfileActionCommentLike(ProfileActionCommentLike comment);
        void RemoveProfileActionCommentLike(long id, long profileId);
    }
}
