

using HeBianGu.Applications.ControlBase.Demo;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Application.BlurWindow
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow shellWindow = new MainWindow();

            shellWindow.Show();

            base.OnStartup(e);
        }


        protected override void ConfigureServices(IServiceCollection services)
        {
            //  Do ：注册本地化配置读写服务
            services.AddSingleton<IThemeLocalizeService, AssemblyDomain>();

            ////  Do ：注册日志服务
            //services.AddSingleton<ILogService, AssemblyDomain>();


        }

        protected override void Configure(IApplicationBuilder app)
        {
            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = Color.FromRgb(0x64, 0x76, 0x87);
                l.SmallFontSize = 15D;
                l.LargeFontSize = 18D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = 5;

                l.AnimalSpeed = 5000;

                l.AccentColorSelectType = 0;

                l.IsUseAnimal = true;

                l.ThemeType = ThemeType.Light;

                l.Language = Language.Chinese;
            });
        }
    }
}
