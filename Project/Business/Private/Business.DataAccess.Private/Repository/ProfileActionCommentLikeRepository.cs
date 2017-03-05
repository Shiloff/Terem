using System.Data.Entity;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository;
using DataAccess.Private.Repository;

namespace Business.DataAccess.Private.Repository
{
    internal class ProfileActionCommentLikeRepository : RepositoryBase<ProfileActionCommentLike, long>, IProfileActionCommentLikeRepository
    {
        public ProfileActionCommentLikeRepository(DbContext context) : base(context)
        {
        }

        private EFDBContext DbContext => Context as EFDBContext;
    }
}