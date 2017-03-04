using System.Data.Entity;

namespace Business.DataAccess.Public.DatabaseContext.Factory
{
    public interface IDbContextFactory<out TContext> where TContext : DbContext
    {
        TContext Create();
    }
}
