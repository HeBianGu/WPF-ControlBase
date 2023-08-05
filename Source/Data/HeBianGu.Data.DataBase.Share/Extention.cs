using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Data.DataBase;
using System.Reflection;
using System.Linq;
using HeBianGu.Systems.Identity;
using HeBianGu.Systems.Operation;
using HeBianGu.Systems.Logger;
using Microsoft.EntityFrameworkCore;

namespace System
{
    public static class Extention
    {
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddDataBaseInitService(this IServiceCollection service)
        //{
        //    service.AddWindowCaptionViewPresenter();
        //    service.AddSingleton<IDataBaseInitService, DataBaseInitService>();
        //    //action?.Invoke(AboutViewPresenter.Instance);
        //    //SystemSetting.Instance?.Add(AboutViewPresenter.Instance);
        //}

        public static void AddSystemRepository(this IServiceCollection serviecs)
        {
            serviecs.AddSystemRepository<DataContext>();
        }

        public static void AddSystemRepository<TDbContext>(this IServiceCollection serviecs) where TDbContext : DbContext
        {
            serviecs.AddSingleton<IStringRepository<hi_dd_author>, DbContextRepository<TDbContext, hi_dd_author>>();
            serviecs.AddSingleton<IStringRepository<hi_dd_role>, DbContextRepository<TDbContext, hi_dd_role>>();
            serviecs.AddSingleton<IStringRepository<hi_dd_user>, DbContextRepository<TDbContext, hi_dd_user>>();
            serviecs.AddSingleton<IStringRepository<hi_dd_operation>, DbContextRepository<TDbContext, hi_dd_operation>>();
            serviecs.AddSingleton<IStringRepository<hl_dm_fatal>, DbContextRepository<TDbContext, hl_dm_fatal>>();
            serviecs.AddSingleton<IStringRepository<hl_dm_debug>, DbContextRepository<TDbContext, hl_dm_debug>>();
            serviecs.AddSingleton<IStringRepository<hl_dm_error>, DbContextRepository<TDbContext, hl_dm_error>>();
            serviecs.AddSingleton<IStringRepository<hl_dm_info>, DbContextRepository<TDbContext, hl_dm_info>>();
            serviecs.AddSingleton<IStringRepository<hl_dm_warn>, DbContextRepository<TDbContext, hl_dm_warn>>();
        }
    }
}
