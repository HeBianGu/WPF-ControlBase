using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Logger;
using System.Reflection;
using System.Linq;
using HeBianGu.Systems.Repository;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddLogger(this IServiceCollection service, Action<ILogServiceOption> action = null)
        {
            service.AddSingleton<ILogService, LogService>();
            action?.Invoke(LogService.Instance);
            SystemSetting.Instance.Add(LogService.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDbLogger(this IServiceCollection service, Action<ILogDbServiceOption> action = null)
        {
            service.AddSingleton<ILogService, LogDbService>();
            action?.Invoke(LogDbService.Instance);
            SystemSetting.Instance.Add(LogDbService.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddLoggerViewPresenter(this IServiceCollection service, Action<ILoggerViewPresenterOption> action = null)
        {
            service.AddSingleton<ILoggerViewPresenter, LoggerViewPresenter>();
            action?.Invoke(LoggerViewPresenter.Instance);
            SystemSetting.Instance.Add(LoggerViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDebugRepositoryViewPresenter(this IServiceCollection service, Action<IRepositoryPresenterOption> action = null)
        {
            service.AddSingleton<IDebugRepositoryViewPresenter, DebugRepositoryViewPresenter>();
            action?.Invoke(DebugRepositoryViewPresenter.Instance);
            //SystemSetting.Instance.Add(DebugRepositoryViewPresenter.Instance);
        }
    }
}
