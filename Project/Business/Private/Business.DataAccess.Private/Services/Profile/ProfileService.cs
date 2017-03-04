using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
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

        public GetContactsResult GetContacts(long profileId, int count = 0)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                //TODO оптимизировать это
                var contacts = uow.Profiles.GetContacts(profileId);
                var result = new GetContactsResult
                {
                    Profiles = contacts.Take(count).ToList(),
                    Count = contacts.Count
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
    }
}