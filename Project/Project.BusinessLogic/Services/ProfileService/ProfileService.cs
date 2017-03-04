using System.Linq;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.UnitOfWork.Factory;

namespace Project.BusinessLogic.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProfileService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Profile GetProfileWithDetails(long id)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Profiles.GetProfileWithDetails(id);
            }
        }


        public Profile GetShortProfile(long id)
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

        public void UpdateProfile(Profile profile, ProfileUpdateMode mode, int[] selectedInteresesId = null)
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
    }
}