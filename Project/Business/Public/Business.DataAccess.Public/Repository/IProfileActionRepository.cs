using System.Collections.Generic;
using Business.DataAccess.Public.Entities;
using DataAccess.Public.Repository;

namespace Business.DataAccess.Public.Repository
{
    public interface IProfileActionRepository : IRepository<ProfileAction, long>
    {
        List<ProfileAction> GetProfileActionsWithComments(long id);

        ProfileAction GetProfileActionWithComments(long id);
    }
}