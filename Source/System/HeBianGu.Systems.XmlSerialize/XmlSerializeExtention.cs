// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Systems.XmlSerialize;

namespace System
{
    public static class XmlSerializeExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddXmlMeta(this IServiceCollection service)
        {
            service.AddSingleton<IMetaSettingService, XmlMetaSettingService>();
        }

        public static void AddXmlSerialize(this IServiceCollection service)
        {
            service.AddSingleton<ISerializerService, XmlSerializerService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseMeta(this IApplicationBuilder service, Action<MetaSetting> action = null)
        {
            action?.Invoke(MetaSetting.Instance);

            SystemSetting.Instance?.Add(MetaSetting.Instance);
        }
    }
}
