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
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceRegistry.Instance.UseMvc();

            //  Do：设置默认主题
            ThemeService.Current.AccentColor =Color.FromRgb(0x1b, 0xa1, 0xe2);

            ThemeService.Current.StartAnimationTheme(1000 * 30);

            MainWindow shellWindow = new MainWindow();

            LoginWindow loginWindow = new LoginWindow();

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

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }


        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Current.Dispatcher.Invoke(() => MessageWindow.ShowSumit(e.Exception.Message, "系统异常",5));

            e.Handled = true;
        }


        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;

            Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));
        }

    }
}
