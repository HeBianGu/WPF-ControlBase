// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Systems.Repository;

namespace System
{
    public static class RepositoryExtention
    {
        /// <summary>
        /// 注册工程保存信息
        /// </summary>
        /// <param name="service"></param>
        public static void AddStudentDemo(this IServiceCollection service)
        {
            //service.AddSingleton<IRepository<Student>, Repository<Student>>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseRepository(this IApplicationBuilder service, Action<RepositorySetting> action = null)
        {
            action?.Invoke(RepositorySetting.Instance);

            SystemSetting.Instance?.Add(RepositorySetting.Instance);
        }
    }


}
