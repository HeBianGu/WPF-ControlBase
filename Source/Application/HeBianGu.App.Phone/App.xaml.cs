using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;

using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.Phone
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override MainWindowBase CreateMainWindow(StartupEventArgs e)
        {
            return new ShellWindow();
        }


        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            //  Do ：启用窗口加载和隐藏动画 需要引用 HeBianGu.Service.Animation
            services.AddWindowAnimation();
            //  Do ：启用消息提示 需要引用 HeBianGu.Control.Message
            services.AddMessageProxy();

            //  Do ：启用和显示右上角主题设置
            services.AddTheme();

            //  Do ：注入领域模型服务
            services.AddSingleton<IAssemblyDomain, AssemblyDomain>();

            services.AddMvc();

        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");

                l.DefaultFontSize = 15D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 45;
                l.RowHeight = 50;
                l.ItemCornerRadius = 5;

                l.AnimalSpeed = 5000;

                l.AccentColorSelectType = 0;

                l.IsUseAnimal = false;

                l.ThemeType = ThemeType.Light;

                l.Language = Language.Chinese;

                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });
        }

    }
}
