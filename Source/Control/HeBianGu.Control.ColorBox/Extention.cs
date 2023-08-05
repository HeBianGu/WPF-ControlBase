using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ColorBox;

namespace System
{
    public static class ColorBoxExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddColorBox(this IServiceCollection service)
        {
            service.AddSingleton<IColorBoxService, ColorBoxService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseColorBox(this IApplicationBuilder service, Action<ColorBoxSetting> action)
        {
            action?.Invoke(ColorBoxSetting.Instance);
            //SystemSetting.Instance.Add(ColorBoxSetting.Instance);
        }
    }
}
