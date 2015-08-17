using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace ZKJL.Identity.EntityFramework.EntityFramework.Repositories
{
    public abstract class IdentityRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<IdentityDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected IdentityRepositoryBase(IDbContextProvider<IdentityDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public abstract class IdentityRepositoryBase<TEntity> : IdentityRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected IdentityRepositoryBase(IDbContextProvider<IdentityDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
