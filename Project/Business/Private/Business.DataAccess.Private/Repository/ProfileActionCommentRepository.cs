using System.Data.Entity;
using System.Linq;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository;
using DataAccess.Private.Repository;

namespace Business.DataAccess.Private.Repository
{
    public class ProfileActionCommentRepository : RepositoryBase<ProfileActionComment, long>,
        IProfileActionCommentRepository
    {
        public ProfileActionCommentRepository(DbContext context) : base(context)
        {
        }

        private EFDBContext DbContext => Context as EFDBContext;


        public ProfileActionComment GetCommentWithData(long id)
        {
            return DbContext.ProfileActionsComments
                .Where(m => m.ProfileActionCommentId == id)
                .Include(m => m.Profile)
                .Include(m => m.ProfileActionsCommentsLikes)
                .FirstOrDefault();
        }
    }
}
