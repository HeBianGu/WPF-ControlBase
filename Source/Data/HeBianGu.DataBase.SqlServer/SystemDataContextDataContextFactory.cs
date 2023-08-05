using HeBianGu.Data.DataBase;

#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using HeBianGu.Systems.Logger;
using HeBianGu.Systems.Operation;
using HeBianGu.Systems.Identity;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
#endif
namespace HeBianGu.DataBase.SqlServer
{
#if NETCOREAPP
    public class DataContextDataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-UQVBO72\\SQLEXPRESS; Trusted_Connection=False; uid=sa; pwd=123456;Initial Catalog=SqlServerMigration; MultipleActiveResultSets=true;");
            return new DataContext(optionsBuilder.Options);
        }
    }
#endif
}
