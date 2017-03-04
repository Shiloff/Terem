using Business.DataAccess.Public.DatabaseContext.Factory;

namespace Business.DataAccess.Private.DatabaseContext.Factory
{
    public class EfDbContextFactory : IDbContextFactory<EFDBContext>
    {
        public EFDBContext Create()
        {
            return new EFDBContext();
        }
    }
}