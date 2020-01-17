using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;

namespace HeBianGu.Application.TouchWindow
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ShellWindow shellWindow = new ShellWindow();

            shellWindow.Show();

            AssemblyDomain.Instance.StartMonitor();

            var domain = this.IServiceCollection.GetService<IAssemblyDomain>();

            domain.StartMonitor();

            base.OnStartup(e);
        }


        protected override void ConfigureServices(IServiceCollection services)
        {
            //  Do：注册Mvc模式
            services.UseMvc();

            //  Do ：注入领域模型服务
            services.AddSingleton<IAssemblyDomain, AssemblyDomain>();

            //  Do ：注册本地化配置读写服务
            services.AddSingleton<IThemeLocalizeService, ThemeLocalizeService>();

            ////  Do ：注册日志服务
            //services.AddSingleton<ILogService, AssemblyDomain>();

        }

        protected override void Configure(IApplicationBuilder app)
        {
            //  Do：设置默认主题
            app.UseLocalTheme(l =>
            {
                l.AccentColor = Color.FromRgb(0, 131, 239);
                l.SmallFontSize = 15D;
                l.LargeFontSize = 18D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = new CornerRadius(17.5);

                l.AnimalSpeed = 5000;
                l.AccentColorSelectType = 0;
                l.IsUseAnimal = false;

                l.ThemeType = ThemeType.Light;

                l.Language = Language.Chinese;
            });

            app.UseTheme(l => l.DialogCoverBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)) { Opacity = 0.80 });
        }

    }
}
