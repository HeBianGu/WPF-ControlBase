using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Operation;
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
        public static void AddOparationViewPresenter(this IServiceCollection service, Action<IOparationViewPresenterOption> action = null)
        {
            service.AddSingleton<IOparationViewPresenter, OparationViewPresenter>();
            //LoginManagerViewPresenterProxy.Instance.AddPersenter(OparationViewPresenter.Instance);
            action?.Invoke(OparationViewPresenter.Instance);
            SystemSetting.Instance.Add(OparationViewPresenter.Instance);
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddLoginOparationViewPresenter(this IServiceCollection service, Action<ILoginOparationViewPresenterOption> action = null)
        {
            service.AddSingleton<ILoginOparationViewPresenter, LoginOparationViewPresenter>();
            action?.Invoke(LoginOparationViewPresenter.Instance);
            SystemSetting.Instance.Add(LoginOparationViewPresenter.Instance);
        }
    }
}
