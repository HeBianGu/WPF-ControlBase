
using HeBianGu.Base.WpfBase;
using System.Linq;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using HeBianGu.Data.DataBase;
using System.Windows.Input;
using System;
using Microsoft.Extensions.Options;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif
namespace HeBianGu.DataBase.Sqlite
{
    public class SqliteDataBaseInitService<TDbContext> : SingleDataBaseInitServiceBase<TDbContext> where TDbContext : DbContext
    {
        protected override DbContextOptionsBuilder<TDbContext> GetOptionsBuilder(string connect)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlite(connect);
            return optionsBuilder;
        }

        protected override IDataBaseSetting GetSetting()
        {
            return SqliteSetting.Instance;
        }
    }

    //public class SqliteDataBaseInitService : SqliteDataBaseInitService<SystemDataContext>
    //{

    //}
}
