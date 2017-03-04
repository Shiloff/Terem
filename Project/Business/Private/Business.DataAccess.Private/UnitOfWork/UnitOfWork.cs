using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Private.Repository;
using Business.DataAccess.Public.DatabaseContext.Factory;
using Business.DataAccess.Public.Repository;
using Business.DataAccess.Public.UnitOfWork;

namespace Business.DataAccess.Private.UnitOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly EFDBContext _context;

        public IProfileRepository Profiles { get; }

        public UnitOfWork(IDbContextFactory<EFDBContext> factory)
        {
            _context = factory.Create();

            Profiles = new ProfileRepository(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
