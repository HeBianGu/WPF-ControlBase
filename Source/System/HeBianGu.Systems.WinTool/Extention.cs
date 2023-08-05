using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.WinTool;
using System.Reflection;
using System.Linq;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddWinToolViewPresenter(this IServiceCollection service, Action<IWinToolViewPresenterOption> action = null)
        {
            //service.AddWindowStatusViewPresenter();
            service.AddSingleton<IWinToolViewPresenter, WinToolViewPresenter>();
            action?.Invoke(WinToolViewPresenter.Instance);
            //WindowStatusViewPresenter.Instance.AddPersenter(WinToolViewPresenter.Instance);
            //SystemSetting.Instance.Add(WinToolViewPresenter.Instance);

            var collection = typeof(ProcessViewPresenter).Assembly.GetTypes().Where(x => typeof(ProcessViewPresenter).IsAssignableFrom(x));
            foreach (var item in collection)
            {
                if (item.IsAbstract) continue;
                var instance = Activator.CreateInstance(item) as IInvokePresenter;
                WinToolViewPresenter.Instance.AddPersenter(instance);
            }
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddWinSpecialFolderViewPresenter(this IServiceCollection service, Action<IWinSpecialFolderViewPresenterOption> action = null)
        {
            //service.AddWindowStatusViewPresenter();
            service.AddSingleton<IWinSpecialFolderViewPresenter, WinSpecialFolderViewPresenter>();
            action?.Invoke(WinSpecialFolderViewPresenter.Instance);
            //WindowStatusViewPresenter.Instance.AddPersenter(WinSpecialFolderViewPresenter.Instance);
            //SystemSetting.Instance.Add(WinSpecialFolderViewPresenter.Instance);

            var collection = typeof(SpecialFolderViewPresenterBase).Assembly.GetTypes().Where(x => typeof(SpecialFolderViewPresenterBase).IsAssignableFrom(x));
            foreach (var item in collection)
            {
                if (item.IsAbstract) continue;
                var instance = Activator.CreateInstance(item) as IInvokePresenter;
                WinSpecialFolderViewPresenter.Instance.AddPersenter(instance);
            }

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddFastFileViewPresenter(this IServiceCollection service, Action<IFastFileViewPresenterOption> action = null)
        {
            //service.AddWindowStatusViewPresenter();
            service.AddSingleton<IFastFileViewPresenter, FastFileViewPresenter>();
            action?.Invoke(FastFileViewPresenter.Instance);
            //WindowStatusViewPresenter.Instance.AddPersenter(FastFileViewPresenter.Instance);
            //SystemSetting.Instance.Add(FastFileViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddExtensionViewPresenter(this IServiceCollection service, Action<IExtensionViewPresenterOption> action = null)
        {
            //service.AddWindowStatusViewPresenter();
            service.AddSingleton<IExtensionViewPresenter, ExtensionViewPresenter>();
            action?.Invoke(ExtensionViewPresenter.Instance);
            //WindowStatusViewPresenter.Instance.AddPersenter(ExtensionViewPresenter.Instance);
            //SystemSetting.Instance.Add(ExtensionViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddFavoriteViewPresenter(this IServiceCollection service, Action<IFavoriteViewPresenterOption> action = null)
        {
            //service.AddWindowStatusViewPresenter();
            service.AddSingleton<IFavoriteViewPresenter, FavoriteViewPresenter>();
            action?.Invoke(FavoriteViewPresenter.Instance);
            //WindowStatusViewPresenter.Instance.AddPersenter(FavoriteViewPresenter.Instance);
            //SystemSetting.Instance.Add(FavoriteViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddRecenterViewPresenter(this IServiceCollection service, Action<IRecenterViewPresenterOption> action = null)
        {
            //service.AddWindowStatusViewPresenter();
            service.AddSingleton<IRecenterViewPresenter, RecenterViewPresenter>();
            action?.Invoke(RecenterViewPresenter.Instance);
            //WindowStatusViewPresenter.Instance.AddPersenter(RecenterViewPresenter.Instance);
            //SystemSetting.Instance.Add(RecenterViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddComputerInfosViewPresenter(this IServiceCollection service, Action<IComputerViewPresenterOption> action = null)
        {
            service.AddSingleton<IComputerViewPresenter, ComputerViewPresenter>();
            action?.Invoke(ComputerViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddCounterViewPresenter(this IServiceCollection service, Action<ICounterViewPresenterOption> action = null)
        {
            service.AddSingleton<ICounterViewPresenter, CounterViewPresenter>();
            action?.Invoke(CounterViewPresenter.Instance);
            SystemSetting.Instance.Add(CounterViewPresenter.Instance);
        }
    }
}
