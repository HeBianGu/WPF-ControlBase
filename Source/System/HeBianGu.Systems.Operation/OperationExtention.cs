// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Systems.Operation;

namespace System
{
    public static class OperationExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddOperation(this IServiceCollection service)
        {
            service.AddSingleton<IOperationService, OperationService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseOperation(this IApplicationBuilder service, Action<OperationConfig> action)
        {
            action?.Invoke(OperationConfig.Instance);
        }
    }
}
