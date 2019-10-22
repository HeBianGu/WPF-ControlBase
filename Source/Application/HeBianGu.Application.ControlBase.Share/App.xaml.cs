using HeBianGu.Applications.ControlBase.LinkWindow.Control;
using HeBianGu.Applications.ControlBase.LinkWindow.Controler;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ApplicationBase
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow shellWindow = new MainWindow();

            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Title = this.GetWpfControlLibVersonInfo();

            var result = loginWindow.ShowDialog();

            if (result.HasValue && result.Value)
            {
                shellWindow.Show();
            }
            else
            {
                shellWindow.Close();
            }

            base.OnStartup(e);

        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            //  Do：注册Mvc模式
            services.UseMvc();
        }

        protected override void Configure(IApplicationBuilder app)
        {
            //  Do：设置默认主题
            app.UseTheme(l =>
            {
                l.AccentColor = Color.FromRgb(0x64, 0x76, 0x87);
                l.SmallFontSize = 15D;
                l.LargeFontSize = 18D;
                l.FontSize = FontSize.Small;

                l.ItemHeight = 35;
                //l.ItemWidth = 120;
                l.ItemCornerRadius = new CornerRadius(17.5);
                l.StartAnimationTheme(1000 * 10);
            });
        }
    }
}
