using HeBianGu.Base.WpfBase;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif

namespace HeBianGu.Data.DataBase
{
    /// <summary>
    /// 主键为Guid类型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class DbContextRepository<TDbContext, TEntity> : RepositoryBase<TDbContext, TEntity, string>, IStringRepository<TEntity> where TEntity : StringEntityBase where TDbContext : DbContext
    {
        public DbContextRepository(TDbContext dbContext) : base(dbContext)
        {

        }
    }
}
