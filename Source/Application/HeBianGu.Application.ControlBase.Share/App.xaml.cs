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
            ServiceRegistry.Instance.UseMvc();

            //  Do：设置默认主题
            ThemeService.Current.AccentColor =Color.FromRgb(0x1b, 0xa1, 0xe2);

            ThemeService.Current.StartAnimationTheme(1000 * 30);

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
    }
}
