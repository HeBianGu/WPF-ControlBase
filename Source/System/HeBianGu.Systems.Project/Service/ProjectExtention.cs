// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Systems.Project;

namespace System
{
    public static class ProjectExtention
    {
        /// <summary>
        /// 注册工程保存信息
        /// </summary>
        /// <param name="service"></param>
        public static void AddProjectDefault(this IServiceCollection service)
        {
            service.AddSingleton<IProjectService, ProjectService>();
        }

        /// <summary>
        /// 工程默认配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseProject(this IApplicationBuilder service, Action<IProjectConfig> action)
        {
            IProjectService find = ServiceRegistry.Instance.GetInstance<IProjectService>();

            action?.Invoke(find as IProjectConfig);
        }
    }
}
