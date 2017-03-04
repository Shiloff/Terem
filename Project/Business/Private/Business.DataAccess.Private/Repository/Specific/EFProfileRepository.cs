using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Repository.Specific
{
    public class EFProfileRepository : IProfileRepository
    {
        EFDBContext context = new EFDBContext();

        public IQueryable<Profile> Profiles
        {
            get
            {
                return context.Profiles
                    .Include(m => m.Sex)
                    .Include(m => m.SexWho)
                    .Include(m => m.Alcohol)
                    .Include(m => m.Smoking)
                    .Include(m => m.Animals)
                    .Include(m => m.Intereses)
                    .Include(m => m.Activity);
            }
        }
        public void NewProfile(Profile profile)
        {
            context.Profiles.Add(profile);
            context.SaveChanges();
        }

        public Profile GetProfile(long? profileId)
        {
            return context.Profiles.Where(m => m.ProfileId == profileId)
                .Include(m => m.Sex)
                .Include(m => m.SexWho)
                .Include(m => m.Alcohol)
                .Include(m => m.Smoking)
                .Include(m => m.Animals)
                .Include(m => m.Intereses)
                .Include(m => m.Activity)
                .FirstOrDefault();
        }

        public void RemoveInteres(Profile profile, ProfileInteres profileInteres)
        {
            var interes = context.ProfileInteres.Find(profileInteres.ProfileInteresId);
            var interesToProfile = context.Profiles.Include(m => m.Intereses).Where(m => m.ProfileId == profile.ProfileId).FirstOrDefault();

            if ((interesToProfile != null) && (interes != null) && (interesToProfile.Intereses.Contains(interes)))
            {
                interesToProfile.Intereses.Remove(interes);
                context.SaveChanges();
            }
        }

       public bool IsProfileExists(long? profileId)
        {
            bool result = false;
            if (context.Profiles.Find(profileId) != null)
            {
                result = true;
            }
            return result;
        }
        public List<Profile> GetContacts(long profileId, int count = 0)
        {
            var query = this.Profiles
               .Where(m => m.New == false)
               .Where(m => m.ProfileId != profileId)
               .Where(m => m.MyMessage.Where(k => k.ProfileIdTo == profileId).Count() > 0
                   || m.MessageForMe.Where(k => k.ProfileIdFrom == profileId).Count() > 0)
               .OrderBy(m => m.LastName)
               .ThenBy(m => m.ProfileId)
               .AsQueryable();

            if (count > 0)
                query = query.Take(count);
            var result = query
                .ToList();
            return result;
        }

        private IQueryable<Profile> FindProfilesQuery(Profile profile, FindProfilesParams param)
        {
            return this.Profiles
                .Where(m => m.New == false)
                .Where(m => m.ProfileId != profile.ProfileId)
                .Include(m=>m.Intereses)
                .OrderByDescending(m => m.LastName)
                .AsQueryable();
        }
        public List<Profile> FindProfiles(Profile profile, FindProfilesParams param)
        {
            List<Profile> result;
            var query = FindProfilesQuery(profile, param);
            if (param.Take != 0)
            {
                query = query.Skip(param.Skip).Take(param.Take);
            }
            result = query.ToList();
            return result;
        }
        public long GetFindProfilesCount(Profile profile, FindProfilesParams param)
        {
            return FindProfilesQuery(profile, param).Count();
        }
        #region ProfileActions
        public IQueryable<ProfileAction> GetProfileActionsQuery(long profileId)
        {
            return context.ProfileActions.Where(m => m.ProfileId == profileId).AsQueryable();
        }
        public int GetProfileActionsCount(long profileId)
        {
            return GetProfileActionsQuery(profileId).Count();
        }

        public void AddProfileAction(ProfileAction addAction)
        {
            Profile profile = context.Profiles.Where(m => m.ProfileId == addAction.ProfileId).FirstOrDefault();
            if (profile != null)
            {
                addAction.Date = addAction.Date != null ? addAction.Date : DateTime.Now;
                context.ProfileActions.Add(addAction);
                context.SaveChanges();
            }
        }

        public void AddProfileActionLike(long profileActionId, long profileId)
        {
            bool profileExist = IsProfileExists(profileId);
            ProfileAction profileAction = context
                .ProfileActions
                .Where(m => m.ProfileActionId == profileActionId)
                .FirstOrDefault();
            ProfileActionLike profileActionLike = context
                .ProfileActionsLikes
                .Where(m => m.ProfileActionId == profileActionId && m.ProfileId == profileId)
                .FirstOrDefault();
            if ((profileExist) && (profileAction != null) && (profileActionLike == null))
            {
                context.ProfileActionsLikes.Add(new ProfileActionLike
                {
                    Date = DateTime.Now,
                    ProfileId = profileId,
                    ProfileActionId = profileActionId
                });
                context.SaveChanges();
            }
        }
        public void RemoveProfileActionLike(long profileActionId, long profileId)
        {
            bool profileExist = IsProfileExists(profileId);
            ProfileAction profileAction = context
                .ProfileActions
                .Where(m => m.ProfileActionId == profileActionId)
                .FirstOrDefault();
            ProfileActionLike profileActionLike = context.ProfileActionsLikes
               .Where(m => m.ProfileActionId == profileActionId && m.ProfileId == profileId).FirstOrDefault();
            if((profileExist) && (profileAction != null) &&  (profileActionLike!=null))
            {
                context.ProfileActionsLikes.Remove(profileActionLike);
                context.SaveChanges();
            }
        }
        #endregion
        #region ProfileActionComments
        public List<ProfileActionComment> GetProfileActionsComments(long profileActionId)
        {
            return context.ProfileActionsComments.Where(m => m.ProfileActionId == profileActionId)
                    .Include(m => m.Profile)
                    .Include(m => m.ProfileActionsCommentsLikes)
                    .ToList();
        }
        public ProfileActionComment GetProfileActionsComment(long profileActionCommentId)
        {
            return context.ProfileActionsComments
                .Where(m => m.ProfileActionCommentId == profileActionCommentId)
                .Include(m=>m.Profile)
                .Include(m => m.ProfileActionsCommentsLikes)
                .FirstOrDefault();
        }
        public long AddProfileActionComment(ProfileActionComment addComment)
        {
            ProfileAction profileAction = context
               .ProfileActions
               .Where(m => m.ProfileActionId == addComment.ProfileActionId)
               .FirstOrDefault();
            if (profileAction != null)
            {
                context.ProfileActionsComments.Add(addComment);
                context.SaveChanges();
            }
            return addComment.ProfileActionCommentId;
        }
        public void RemoveProfileActionComment(long removeComment)
        {

            ProfileActionComment RemoveComment = context.ProfileActionsComments.Find(removeComment);
            if (RemoveComment != null)
            {
                context.ProfileActionsComments.Remove(RemoveComment);
                context.SaveChanges();
            }
        }
        public void AddProfileActionCommentLike(long profileActionCommentId, long profileId)
        {
            bool profileExist = IsProfileExists(profileId);
            ProfileActionComment profileAction = context
                .ProfileActionsComments
                .Where(m => m.ProfileActionCommentId == profileActionCommentId)
                .FirstOrDefault();
            ProfileActionCommentLike profileActionCommentLike = context.ProfileActionsCommentsLikes
               .Where(m => m.ProfileActionCommentId == profileActionCommentId && m.ProfileId == profileId).FirstOrDefault();
            if ((profileExist) && (profileAction != null) && (profileActionCommentLike == null))
            {
                context.ProfileActionsCommentsLikes.Add(new ProfileActionCommentLike
                {
                    Date = DateTime.Now,
                    ProfileId = profileId,
                    ProfileActionCommentId = profileActionCommentId
                });
                context.SaveChanges();
            }
        }
        public void RemoveProfileActionCommentLike(long profileActionCommentId, long profileId)
        {
            bool profileExist = IsProfileExists(profileId);
            ProfileActionComment profileAction = context
                .ProfileActionsComments
                .Where(m => m.ProfileActionCommentId == profileActionCommentId)
                .FirstOrDefault();
            ProfileActionCommentLike profileActionCommentLike = context.ProfileActionsCommentsLikes
              .Where(m => m.ProfileActionCommentId == profileActionCommentId && m.ProfileId == profileId).FirstOrDefault();
            if ((profileExist) && (profileAction != null) && (profileActionCommentLike != null))
            {
                context.ProfileActionsCommentsLikes.Remove(profileActionCommentLike);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
