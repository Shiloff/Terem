using Business.DataAccess.Public.Entities;
using DataAccess.Public.Repository;

namespace Business.DataAccess.Public.Repository
{
    public interface IProfileActionCommentRepository : IRepository<ProfileActionComment, long>
    {

        ProfileActionComment GetCommentWithData(long id);
    }
}