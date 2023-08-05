using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Guide;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Systems.Setting;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.App.DownLoad
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

            ////  Do ：注入领域模型服务
            //services.AddSingleton<IAssemblyDomain, AssemblyDomain>();

            services.AddWindowDialog();

            //services.AddWindowAnimation();

            //services.AddStudentDemo();

            services.AddMessageProxy();

            services.AddSetting();

            services.AddXmlSerialize();
            services.AddXmlMeta();

            //  Do ：启用启动窗口 需要添加引用HeBianGu.Window.Start
            services.AddStart(l =>
            {
                l.Product = "DownLoad";
                l.ProductFontSize = 70;
            });

            ////  Do ：启用启动窗口 需要添加引用HeBianGu.Systems.Identity
            //services.AddIdentity();

            services.AddSettingViewPrenter();
            services.AddGuideViewPresenter();
            //  Do ：启用和显示右上角主题设置
            services.AddThemeRightViewPresenter();
            services.AddWindowCaptionViewPresenter(x =>
            {
                x.AddPersenter(GuideViewPresenter.Instance);
                x.AddPersenter(SettingViewPresenter.Instance);
                x.AddPersenter(ThemeRightToolViewPresenter.Instance);
            });
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
                l.FontSize = FontSize.Small;
                l.ItemHeight = 36;
                l.RowHeight = 40;
                //l.ItemWidth = 120;
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
