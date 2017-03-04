
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.DatabaseContext.Factory;
using Business.DataAccess.Public.UnitOfWork;
using Business.DataAccess.Public.UnitOfWork.Factory;

namespace Business.DataAccess.Private.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory<EFDBContext> _factory;

        public UnitOfWorkFactory(IDbContextFactory<EFDBContext> factory)
        {
            _factory = factory;
        }
        public IUnitOfWork Create()
        {
            return new UnitOfWork(_factory);
        }
    }
}
