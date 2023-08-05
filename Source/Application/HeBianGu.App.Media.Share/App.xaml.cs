using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Guide;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Setting;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.Media
{
    /// <summary>
    /// Interaction logic for App.xaml
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

            services.AddMvc();
            services.AddWindowDialog();
            services.AddMessageProxy();
            services.AddSetting();
            services.AddXmlSerialize();
            services.AddXmlMeta();

            ////  Do ：启用启动窗口 需要添加引用HeBianGu.Window.Start
            //services.AddStart();

            ////  Do ：启用启动窗口 需要添加引用HeBianGu.Systems.Identity
            //services.AddIdentity();

            #region - WindowCaption -
            services.AddGuideViewPresenter();
            services.AddSettingViewPrenter();
            services.AddThemeRightViewPresenter();
            services.AddWindowCaptionViewPresenter(x =>
            {
                x.AddPersenter(MoreViewPresenter.Instance);
                x.AddPersenter(GuideViewPresenter.Instance);
                x.AddPersenter(SettingViewPresenter.Instance);
                x.AddPersenter(ThemeRightToolViewPresenter.Instance);
            });
            #endregion
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            app.UseMainWindowSetting();

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");
                l.DefaultFontSize = 14D;
                l.FontSize = FontSize.Normal;
                l.ItemHeight = 36;
                l.RowHeight = 40;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 5;
                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;
                l.ThemeType = ThemeType.DarkGray;
                l.StyleType = StyleType.Single;
                l.Language = Language.Chinese;
                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });
        }

    }
}
