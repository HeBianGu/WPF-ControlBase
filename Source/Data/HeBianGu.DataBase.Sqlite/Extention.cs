using HeBianGu.Base.WpfBase;
using HeBianGu.DataBase.Sqlite;
using HeBianGu.Data.DataBase;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Sqlite;
#endif

namespace System
{
    public static class SqliteExtention
    {
        public static void AddDbContext(this IServiceCollection services, Action<ISqliteOption> action = null)
        {
            services.AddDbContext<DataContext>(action);
        }

        public static void AddDbContext<TDbContext>(this IServiceCollection services, Action<ISqliteOption> action) where TDbContext : DbContext
        {
            action?.Invoke(SqliteSetting.Instance);
            services.AddXmlSerialize();
            SqliteSetting.Instance.Load();
            string connect = SqliteSetting.Instance.GetConnect();

#if NETCOREAPP
            //var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            //optionsBuilder.UseSqlite(connect);
            //SystemDataContext abcDataContext = new TDbContext(optionsBuilder.Options);
            //services.AddSingleton(abcDataContext);
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlite(connect);
            var abcDataContext = Activator.CreateInstance(typeof(TDbContext), new object[] { optionsBuilder.Options }) as TDbContext;
            //T abcDataContext = new T(optionsBuilder.Options);
            services.AddSingleton(abcDataContext);
#endif
            SystemSetting.Instance.Add(SqliteSetting.Instance);
        }

        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDataBaseInitService(this IServiceCollection service)
        {
            service.AddDataBaseInitService<DataContext>();
        }

        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDataBaseInitService<TDbContext>(this IServiceCollection service) where TDbContext : DbContext
        {
            service.AddSingleton<IDataBaseInitService, SqliteDataBaseInitService<TDbContext>>();
            service.AddSingleton<IDataBaseSaveService, DataBaseSaveService<TDbContext>>();
        }
    }
}
