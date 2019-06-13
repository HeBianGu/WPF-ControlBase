using HeBianGu.Base.WpfBase.Color;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Applications.ControlBase.Demo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => MessageWindow.ShowSumit(e.Exception.Message, "系统异常", 5));

            e.Handled = true;
        }


        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;

            Application.Current.Dispatcher.Invoke(() => MessageWindow.ShowSumit("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            //  Do：设置默认主题
            ThemeService.Current.AccentColor = Color.FromRgb(0x33, 0x99, 0x33);

            ThemeService.Current.StartAnimationTheme(2000);


            base.OnStartup(e);
        }
    }
}
