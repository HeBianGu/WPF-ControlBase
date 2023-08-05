using HeBianGu.Base.WpfBase;
using HeBianGu.DataBase.SqlServer;
//using HeBianGu.Data.DataBase;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.SqlServer;
using HeBianGu.Data.DataBase;
#endif

namespace System
{
    public static class SqlServerExtention
    {
        public static void AddDbContext(this IServiceCollection services, Action<ISqlServerOption> action)
        {
            AddDbContext<DataContext>(services, action);
            //action.Invoke(SqlServerSetting.Instance);
            //            services.AddXmlSerialize();
            //            SqlServerSetting.Instance.Load();
            //            string connect = SqlServerSetting.Instance.GetConnect();

            //#if NETCOREAPP
            //            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            //            optionsBuilder.UseSqlServer(connect);
            //            DataContext abcDataContext = new DataContext(optionsBuilder.Options);
            //            services.AddSingleton(abcDataContext);
            //#endif

            //            SystemSetting.Instance.Add(SqlServerSetting.Instance);
        }

        public static void AddDbContext<TDbContext>(this IServiceCollection services, Action<ISqlServerOption> action) where TDbContext : DbContext
        {
            action.Invoke(SqlServerSetting.Instance);
            services.AddXmlSerialize();
            SqlServerSetting.Instance.Load();
            string connect = SqlServerSetting.Instance.GetConnect();
#if NETCOREAPP
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlServer(connect);
            var abcDataContext = Activator.CreateInstance(typeof(TDbContext), new object[] { optionsBuilder.Options }) as TDbContext;
            services.AddSingleton(abcDataContext);
#endif
            SystemSetting.Instance.Add(SqlServerSetting.Instance);
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
            service.AddSingleton<IDataBaseInitService, SqlServerDataBaseInitService<TDbContext>>();
            service.AddSingleton<IDataBaseSaveService, DataBaseSaveService<TDbContext>>();
        }
    }
}
