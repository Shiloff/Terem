using System;
using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Services.Contact;
using Business.DataAccess.Public.Services.Profile;
using Business.DataAccess.Public.UnitOfWork.Factory;

namespace Business.DataAccess.Private.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProfileService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void AddProfile(Public.Entities.Profile profile)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                uow.Profiles.Add(profile);
                uow.Complete();
            }
        }

        public Public.Entities.Profile GetProfileWithDetails(long id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Profiles.GetProfileWithDetails(id);
            }
        }


        public Public.Entities.Profile GetShortProfile(long id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Profiles.GetShortProfile(id);
            }
        }

        public GetContactsResult GetShortContacts(long profileId, int count = 0)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                //TODO оптимизировать это
                var contacts = uow.Profiles.GetContacts(profileId, new ContactFilter());
                var result = new GetContactsResult
                {
                    Profiles = contacts.Item1.Take(count).ToList(),
                    Count = contacts.Item2
                };
                return result;
            }
        }

        public void UpdateProfile(Public.Entities.Profile profile, ProfileUpdateMode mode, int[] selectedInteresesId = null)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                uow.Profiles.Update(profile, mode);

                if (mode == ProfileUpdateMode.Dop)
                {
                    uow.Profiles.UpdateIntereses(profile.ProfileId, selectedInteresesId);
                }

                uow.Complete();
            }
        }

        public List<ProfileAction> GetProfileActions(long id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.ProfileActions.GetProfileActionsWithComments(id).ToList();
            }
        }

        public ProfileAction GetProfileAction(long id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.ProfileActions.GetProfileActionWithComments(id);
            }
        }

        public ProfileAction AddProfileAction(ProfileAction addAction, long profileId)
        {

            if (string.IsNullOrWhiteSpace(addAction?.Text))
            {
                throw new ArgumentException(nameof(addAction));
            }

            if ((profileId != addAction.ProfileWhoId) && (profileId != 0))
            {
                throw new AccessViolationException(nameof(addAction));
            }

            using (var uow = _unitOfWorkFactory.Create())
            {
                uow.ProfileActions.Add(addAction);
                uow.Complete();
                return addAction;
            }
        }

        public void RemoveProfileAction(long id, long profileId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var action = uow.ProfileActions.Get(id);
                if ((profileId != action.ProfileWhoId) && (profileId != action.ProfileId))
                {
                    throw new AccessViolationException(nameof(action));
                }
                uow.ProfileActions.Remove(action);
                uow.Complete();
            }
        }

        public void AddActionLike(long id, long profileId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var like =
                   uow.ProfileActionLikes.Find(m => m.ProfileId == profileId && m.ProfileActionId == id).FirstOrDefault();

                if (like != null)
                {
                    return;
                }

                uow.ProfileActionLikes.Add(new ProfileActionLike()
                {
                    Date = DateTime.Now,
                    ProfileId = profileId,
                    ProfileActionId = id
                });
                uow.Complete();
            }
        }

        public void RemoveActionLike(long id, long profileId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var like =
                    uow.ProfileActionLikes.Find(m => m.ProfileId == profileId && m.ProfileActionId == id).FirstOrDefault();

                if (like == null)
                {
                    return;
                }

                if (profileId != like.ProfileId)
                {
                    throw new AccessViolationException(nameof(like));
                }
                uow.ProfileActionLikes.Remove(like);
                uow.Complete();
            }
        }

        public long AddProfileActionComment(ProfileActionComment comment)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                uow.ProfileActionComments.Add(comment);
                uow.Complete();

                return comment.ProfileActionCommentId;
            }
        }

        public ProfileActionComment GetProfileActionsComment(long id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.ProfileActionComments.GetCommentWithData(id);
            }
        }

        public void RemoveProfileActionsComment(long id, long profileId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var actionComment = uow.ProfileActionComments
                    .Find(m => m.ProfileId == profileId && m.ProfileActionCommentId == id)
                    .FirstOrDefault();

                if (actionComment == null)
                {
                    return;
                }
                uow.ProfileActionComments.Remove(actionComment);

                uow.Complete();
            }
        }

        public void AddProfileActionCommentLike(ProfileActionCommentLike comment)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var actionCommentLike = uow.ProfileActionCommentLikes
                    .Find(m =>
                        m.ProfileId == comment.ProfileId &&
                        m.ProfileActionCommentId == comment.ProfileActionCommentId)
                    .FirstOrDefault();

                if (actionCommentLike != null)
                {
                    return;
                }

                uow.ProfileActionCommentLikes.Add(comment);
                uow.Complete();
            }
        }

        public void RemoveProfileActionCommentLike(long id, long profileId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var actionCommentLike = uow.ProfileActionCommentLikes
                    .Find(m => m.ProfileId == profileId && m.ProfileActionCommentId == id)
                    .FirstOrDefault();

                if (actionCommentLike == null)
                {
                    return;
                }
                uow.ProfileActionCommentLikes.Remove(actionCommentLike);

                uow.Complete();
            }
        }
    }
}