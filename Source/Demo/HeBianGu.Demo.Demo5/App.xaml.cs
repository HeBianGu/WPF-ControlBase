using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Demo.Demo5
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override MainWindowBase CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            //  Do ：注册动画
            services.AddWindowAnimation();

            //  Do ：注册消息
            services.AddMessageProxy();

            //  Do ：注册窗口配置，注册后窗口右侧有可设置主题的按钮
            services.AddThemeRightViewPresenter();
            services.AddWindowCaptionViewPresenter(x =>
            {
                x.AddPersenter(ThemeRightToolViewPresenter.Instance);
            });

            //  Do ：注册序列化保存接口，注册后主题的配置会保存到本地，再次启动会读取
            services.AddXmlSerialize();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                //  Do ：设置默认主色调
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");
                //  Do ：设置默认字体大小
                l.DefaultFontSize = 14D;
                //  Do ：设置默认字体大小模式
                l.FontSize = FontSize.Small;
                //  Do ：控件默认高度
                l.ItemHeight = 36;
                //  Do ：设置行默认高度
                l.RowHeight = 40;
                //  Do ：设置控件默认圆角
                l.ItemCornerRadius = 5;
                //  Do ：设置启用动态主色调后动态变化的时间间隔
                l.AnimalSpeed = 5000;
                //  Do ：设置主色调列表默认选择项
                l.AccentColorSelectType = 0;
                //  Do ：设置是否启用动态主色调
                l.IsUseAnimal = false;
                //  Do ：设置主题颜色，深色调，浅色调等
                l.ThemeType = ThemeType.Light;
                //  Do ：设置控件的默认显示样式，Default、Light、Single等
                l.StyleType = StyleType.Default;
                //  Do ：设置默认语言（目前不起作用）
                l.Language = Language.Chinese;
                //  Do ：设置主色调类型，渐变方式等
                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });
        }

    }
}
