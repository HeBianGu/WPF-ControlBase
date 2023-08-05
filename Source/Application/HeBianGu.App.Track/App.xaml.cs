// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Guide;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Setting;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.Track
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
            services.AddWindowDialog();
            services.AddWindowAnimation();
            services.AddMessageProxy();
            services.AddXmlSerialize();
            services.AddXmlMeta();
            services.AddSetting();
            ////  Do ：启用启动窗口 需要添加引用HeBianGu.Window.Start
            //services.AddStart(x=>
            //{
            //    x.Title = "Trace";
            //    x.pro = 100;
            //});
            ////  Do ：启用启动窗口 需要添加引用HeBianGu.Systems.Identity
            //services.AddIdentity();
            services.AddPasswordDialog();

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
            app.UseSetting(l =>
            {
                l.Settings.Add(MaterialSetting.Instance);
                l.Settings.Add(DMXSetting.Instance);
                l.Settings.Add(DispatchSetting.Instance);
                l.Settings.Add(UDPSetting.Instance);
                l.Settings.Add(ComSetting.Instance);
                l.Settings.Add(ParamSetting.Instance);
            });

            base.Configure(app);

            app.UseAdorner();

            app.UseScrollViewer(l =>
            {
                l.ScrollBarHeight = 15;
                l.ScrollBarWidth = 15;
            });

            app.UseVlc(l =>
            {
                l.FullScreenType = Control.Vlc.FullScreenType.None;
            });

            app.UseStyle(x =>
            {
                x.StyleType = StyleType.Single;
            });

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF0093FF");
                l.DefaultFontSize = 18D;
                l.FontSize = FontSize.Small;
                l.ItemHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 5;
                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;
                l.ThemeType = ThemeType.DarkGray;
                l.Language = Language.Chinese;
                l.AccentBrushType = AccentBrushType.LinearGradientBrush;
            });
        }
    }
}
