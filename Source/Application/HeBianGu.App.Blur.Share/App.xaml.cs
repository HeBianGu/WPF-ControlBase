using HeBianGu.Applications.ControlBase.Demo;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.Blur
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override System.Windows.Window CreateMainWindow(StartupEventArgs e)
        {
            return new MainWindow();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddMessageDialog();

            services.AddWindowAnimation();

            services.AddTheme();

            services.AddMessage();

            services.AddXmlSerialize();

            services.AddXmlMeta();

            services.AddSetting();

            services.AddSettingPath();

            services.AddSettingViewPrenter();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");

                l.SmallFontSize = 15D;
                l.LargeFontSize = 18D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 38;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 5;

                l.RowHeight = 40;

                l.AnimalSpeed = 5000;

                l.AccentColorSelectType = 0;

                l.IsUseAnimal = true;

                l.ThemeType = ThemeType.Light;

                l.Language = Language.Chinese;

                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });

            var find = Application.Current.TryFindResource(SystemKeys.TransformGroup);

            app.UsePagedDataGrid();

            app.UseMessage();

            app.UseMainWindowSetting();
        }
    }
}
