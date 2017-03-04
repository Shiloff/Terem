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
        IQueryable<Profile> Profiles { get; }
        void NewProfile(Profile profile);
        void RemoveInteres(Profile profile, ProfileInteres profileInteres);
        Profile GetProfile(long? profileId);
        bool IsProfileExists(long? profileId);
        List<Profile> FindProfiles(Profile profile, FindProfilesParams param);
        long GetFindProfilesCount(Profile profile, FindProfilesParams param);
        List<Profile> GetContacts(long profileId, int count = 0);
        #region ProfileActions
        int GetProfileActionsCount(long profileId);
        void AddProfileAction(ProfileAction addAction);
        void AddProfileActionLike(long profileActionId, long profileId);
        void RemoveProfileActionLike(long profileActionId, long profileId);
        
        #endregion

        #region ProfileActionsComments
        List<ProfileActionComment> GetProfileActionsComments(long profileActionId);
        ProfileActionComment GetProfileActionsComment(long profileActionCommentId);
        long AddProfileActionComment(ProfileActionComment addComment);
        void RemoveProfileActionComment(long id);
        void AddProfileActionCommentLike(long profileActionCommentId, long profileId);
        void RemoveProfileActionCommentLike(long profileActionCommentId, long profileId);
        #endregion
    }
}
