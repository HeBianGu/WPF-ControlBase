using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Feedback;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddFeedbackViewPresenter(this IServiceCollection service, Action<IFeedbackViewPresenterOption> action = null)
        {
            service.AddSingleton<IFeedbackViewPresenter, FeedbackViewPresenter>();
            //MoreViewPresenter.Instance.AddPersenter(FeedbackViewPresenter.Instance);
            action?.Invoke(FeedbackViewPresenter.Instance);
            SystemSetting.Instance.Add(FeedbackViewPresenter.Instance);
        }

        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="service"></param>
        //public static void UseFeedback(this IApplicationBuilder service, Action<FeedbackSetting> action)
        //{
        //    action?.Invoke(FeedbackSetting.Instance);
        //    SystemSetting.Instance.Add(FeedbackSetting.Instance);
        //}
    }
}
