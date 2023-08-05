// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Project;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册工程保存信息
        /// </summary>
        /// <param name="service"></param>
        public static void AddProjectDefault(this IServiceCollection service, Action<IProjectOption> action = null)
        {
            service.AddSingleton<IProjectService, ProjectService>();
            SystemSetting.Instance?.Add(ProjectService.Instance);
            IProjectService find = ServiceRegistry.Instance.GetInstance<IProjectService>();
            action?.Invoke(ProjectService.Instance);
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddProjectViewPresenter(this IServiceCollection service, Action<IProjectViewPresenterOption> action = null)
        {
            service.AddSingleton<IProjectViewPresenter, ProjectViewPresenter>();
            action?.Invoke(ProjectViewPresenter.Instance);
            SystemSetting.Instance?.Add(ProjectViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddNewProjectTreeViewPresenter(this IServiceCollection service, Action<INewProjectTreeViewPresenterOption> action = null)
        {
            //service.AddWindowMenuViewPresenter();
            service.AddSingleton<INewProjectTreeViewPresenter, NewProjectTreeViewPresenter>();
            action?.Invoke(NewProjectTreeViewPresenter.Instance);
        }

        ///// <summary>
        ///// 工程默认配置
        ///// </summary>
        ///// <param name="service"></param>
        //public static void UseProject(this IApplicationBuilder service, Action<IProjectConfig> action)
        //{
        //    IProjectService find = ServiceRegistry.Instance.GetInstance<IProjectService>();
        //    action?.Invoke(find as IProjectConfig);
        //}
    }
}
