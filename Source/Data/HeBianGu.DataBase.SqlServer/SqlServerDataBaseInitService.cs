
using HeBianGu.Base.WpfBase;
using System.Linq;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using HeBianGu.Data.DataBase;
using System;
using System.Windows.Input;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif
namespace HeBianGu.DataBase.SqlServer
{
    public class SqlServerDataBaseInitService<TDbContext> : SingleDataBaseInitServiceBase<TDbContext> where TDbContext : DbContext
    {
        protected override IDataBaseSetting GetSetting()
        {
            return SqlServerSetting.Instance;
        }

        protected override DbContextOptionsBuilder<TDbContext> GetOptionsBuilder(string connect)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlServer(connect);
            return optionsBuilder;
        }
    }

    public class SqlServerDataBaseInitService : SqlServerDataBaseInitService<DataContext>
    {

    }
}
