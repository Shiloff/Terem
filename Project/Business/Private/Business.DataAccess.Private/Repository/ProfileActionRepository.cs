using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository;
using DataAccess.Private.Repository;

namespace Business.DataAccess.Private.Repository
{
    internal class ProfileActionRepository : Repository<ProfileAction, long>, IProfileActionRepository
    {
        public ProfileActionRepository(DbContext context) : base(context)
        {
        }

        private EFDBContext DbContext => Context as EFDBContext;

        public List<ProfileAction> GetProfileActionsWithComments(long id)
        {
            var query = DbContext
                .ProfileActions
                .Where(m => m.ProfileId == id)
                .Include(m => m.ProfileWho)
                .Include(m => m.ProfileActionComments.Select(k => k.Profile))
                .Include(m => m.ProfileActionComments.Select(k => k.ProfileActionsCommentsLikes))
                .Include(m => m.Profile)
                .Include(m => m.Apartment.Type)
                .Include(m => m.Apartment.ApartmentPhotos.Select(k => k.Links))
                .Include(m => m.ProfileActionsLikes)
                .OrderByDescending(m => m.Date)
                .AsQueryable();

            return query
                .AsEnumerable()
                .Select(m => new ProfileAction()
                {
                    Date = m.Date,
                    Profile = m.Profile,
                    ProfileWho = m.ProfileWho,
                    Text = m.Text,
                    ProfileActionId = m.ProfileActionId,
                    ProfileId = m.ProfileId,
                    ProfileWhoId = m.ProfileWhoId,
                    ProfileActionTypeId = m.ProfileActionTypeId,
                    ProfileActionComments = m.ProfileActionComments.OrderBy(k => k.Date).ToList(),
                    ProfileActionsLikes = m.ProfileActionsLikes,
                    Apartment = m.Apartment,
                    CommentsCount = m.ProfileActionComments.Count
                })
                .ToList();
        }

        public ProfileAction GetProfileActionWithComments(long id)
        {
            return DbContext.ProfileActions.Where(m => m.ProfileActionId == id)
                .Include(m => m.ProfileActionsLikes)
                .Include(m => m.Profile)
                .Include(m => m.ProfileWho)
                .Include(m => m.Apartment.Type)
                .Include(m => m.Apartment.ApartmentPhotos.Select(k => k.Links))
                .Include(m => m.ProfileActionComments)
                .FirstOrDefault();
        }
    }
}