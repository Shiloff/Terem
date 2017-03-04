using System;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Public.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProfileRepository Profiles { get; }

        void Complete();
    }
}
