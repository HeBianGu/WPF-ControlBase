using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Survey;

namespace System
{
    public static class PropertyGridExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddSurveyViewPresenter(this IServiceCollection service, Action<ISurveyViewPresenterOption> action = null)
        {
            //service.AddMoreViewPresenter();
            service.AddSingleton<ISurveyViewPresenter, SurveyViewPresenter>();
            action?.Invoke(SurveyViewPresenter.Instance);
            SystemSetting.Instance.Add(SurveyViewPresenter.Instance);
            //MoreViewPresenter.Instance.AddPersenter(SurveyViewPresenter.Instance);
        }

        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="service"></param>
        //public static void UseSurvey(this IApplicationBuilder service, Action<SurveySetting> action)
        //{
        //    action?.Invoke(SurveySetting.Instance);
        //    SystemSetting.Instance.Add(SurveySetting.Instance);
        //}
    }


}
