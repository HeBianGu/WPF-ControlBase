using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Systems.Setting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Demo.Demo30
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
            //  Do ：注册窗口配置，注册后窗口右侧有可设置主题的按钮
            services.AddThemeRightViewPresenter();
            //  Do ：注册右上角配置页面
            services.AddSettingViewPrenter();
            services.AddWindowCaptionViewPresenter(x =>
            {
                x.AddPersenter(SettingViewPresenter.Instance);
                x.AddPersenter(ThemeRightToolViewPresenter.Instance);
            });

            services.AddGuideViewPresenter();

            services.AddXmlSerialize();
            services.AddXmlMeta();

            ////  Do ：启用启动窗口 需要添加引用HeBianGu.Window.Start
            //services.AddStart();

            ////  Do ：启用启动窗口 需要添加引用HeBianGu.Systems.Identity
            //services.AddIdentity();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            app.UseMainWindowSetting();

            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = (Color)ColorConverter.ConvertFromString("#FF003D99");

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
