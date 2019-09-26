using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public abstract class ApplicationBase : Application
    {

        public ApplicationBase()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Current.Dispatcher.Invoke(() => MessageWindow.ShowSumit(e.Exception.Message, "系统异常", 5));

            e.Handled = true;

        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;

            Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));
        }

        //protected virtual void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //}


        protected string GetVersonInfo()
        {
           var version=  Assembly.GetEntryAssembly().GetName().Version;

            return $"V{version.Major}.{version.Minor}.{version.Build}";
        }

        protected string GetWpfControlLibVersonInfo()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return $"V{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
